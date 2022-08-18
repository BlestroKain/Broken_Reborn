﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using Intersect.Server.Networking;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Intersect.Server.Database.PlayerData.Players
{
    public static class PlayerRecordCache
    {
        public static Dictionary<string, LeaderboardPage> CachedRecords = new Dictionary<string, LeaderboardPage>();

        /// <summary>
        /// Creates a key from a record search that can be used to cache searches
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="recordId"></param>
        /// <param name="scoringType"></param>
        /// <returns></returns>
        public static string CreateSearchQueryString(RecordType recordType, Guid recordId, RecordScoring scoringType, int page)
        {
            return $"type={Enum.GetName(typeof(RecordType), recordType)}&id={recordId}&scoring={Enum.GetName(typeof(RecordScoring), scoringType)}&page={page}";
        }
    }

    public class LeaderboardPage
    {
        public static readonly long TimeUntilRequery = Options.Instance.RecordOpts.RecordCacheIntervalMinutes * Intersect.Utilities.Timing.Minutes; // 15 minutes
        public List<RecordDto> Records { get; set; }
        public long QueryTimestamp { get; set; }
        public long RequeryTimestamp => QueryTimestamp + TimeUntilRequery;
        public int CurrentPage { get; set; }

        public LeaderboardPage()
        {
        }

        public LeaderboardPage(List<RecordDto> records, long queryTimestamp, int currentPage)
        {
            Records = records;
            QueryTimestamp = queryTimestamp;
            CurrentPage = currentPage;
        }

        public void Cache(string key)
        {
            PlayerRecordCache.CachedRecords[key] = this;
        }
    }

    public class PlayerRecord : IPlayerOwned
    {
        /// <summary>
        /// Locking context to prevent saving this recprd to the db twice at the same time
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private object mLock = new object();

        /// <summary>
        /// Getter for the guild lock
        /// </summary>
        public object Lock => mLock;

        /// <summary>
        /// The amount of records to return per visible page
        /// </summary>
        public static readonly int PageLimit = Options.Instance.RecordOpts.RecordsPerLeaderboardPage;

        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public PlayerRecord() { } // EF

        public PlayerRecord(Guid player, RecordType type, Guid recordId, int initialAmount)
        {
            PlayerId = player;
            Type = type;
            RecordId = recordId;
            Amount = initialAmount;
            ScoreType = RecordScoring.High;
        }

        public PlayerRecord(Guid playerId, RecordType type, Guid recordId, int amount, RecordScoring scoreType)
        {
            PlayerId = playerId;
            Type = type;
            RecordId = recordId;
            Amount = amount;
            ScoreType = scoreType;
        }

        public RecordType Type { get; set; }

        public long Amount { get; set; }

        [ForeignKey(nameof(Player))]
        [JsonIgnore]
        public Guid PlayerId { get; private set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        public Guid RecordId { get; set; }
        
        public RecordScoring ScoreType { get; set; }

        [JsonIgnore]
        public virtual List<RecordTeammateInstance> Teammates { get; set; } = new List<RecordTeammateInstance>();

        [JsonIgnore, NotMapped]
        public int Index;

        /// <summary>
        /// Saves this player record to the player context
        /// </summary>
        public void SaveToContext()
        {
            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                {
                    context.Player_Record.Update(this);
                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public static int GetPlayersPage(Player player, PlayerRecord[] records)
        {
            var firstRecord = records.FirstOrDefault(record => record.PlayerId == player.Id || record.Teammates.Select(tm => tm.PlayerId).Contains(player.Id));
            // Player doesn't have a record yet - give them the first page
            if (firstRecord == default)
            {
                return 0;
            }

            return (int)Math.Ceiling(firstRecord.Index / (double)Options.Instance.RecordOpts.RecordsPerLeaderboardPage);
        }

        public static void SendLeaderboardPageTo(Player player, RecordType recordType, Guid recordId, RecordScoring scoringType, int pageNum)
        {
            var leaderboard = GetLeaderboardPage(player, recordType, recordId, scoringType, pageNum);
            if (leaderboard?.Records == null)
            {
                PacketSender.SendChatMsg(player, "There was an error while opening this leaderboard.", Enums.ChatMessageType.Error);
                return;
            }

            PacketSender.SendRecordPageTo(player, leaderboard.Records, leaderboard.CurrentPage);
        }

        /// <summary>
        /// Gets sorted records of a given type
        /// </summary>
        /// <param name="type">The record type we're looking for</param>
        /// <param name="recordId">The record ID for identification within the record type</param>
        /// <param name="scoreType">The type of scoring we want for the record</param>
        /// <param name="page">The page of record results we're returning</param>
        /// <param name="recordTransformer">A function that returns a string from a long that transforms a long as desired to give the correct output of the record</param>
        /// <returns>A <see cref="LeaderboardPage"/> that we can use to send a packet of</returns>
        public static LeaderboardPage GetLeaderboardPage(Player player, RecordType type, Guid recordId, RecordScoring scoreType, int page, Func<long, string> recordTransformer = null)
        {
            if (recordTransformer == null)
            {
                recordTransformer = (val) => val.ToString();
            }

            // Check if we can just access the cache for results
            var queryString = PlayerRecordCache.CreateSearchQueryString(type, recordId, scoreType, page);
            if (TryGetCachedLeaderboardPage(queryString, out var cachedResults))
            {
                return cachedResults;
            }

            // Otherwise, do a DB search for this new leaderboard lookup and update the cache afterward
            var results = new LeaderboardPage();
            var records = new List<PlayerRecord>();
            Dictionary<Guid, string> playerNameLookup = new Dictionary<Guid, string>();
            using (var context = DbInterface.CreatePlayerContext())
            {
                var allRecords = GetMatchingRecords(context, type, recordId, scoreType);
                // Fetch the first page that contains the player as a holder
                if (page == -1)
                {
                    page = GetPlayersPage(player, allRecords);
                }
                var paginatedRecords = PaginateRecords(allRecords, page);
                // Link up teammate information _after_ paging the set to save on time
                foreach(var record in paginatedRecords)
                {
                    record.Teammates = context.Record_Teammate.Where(teammate => teammate.RecordInstanceId == record.Id).ToList();
                }
                records.AddRange(PaginateRecords(allRecords, page));
                playerNameLookup = GetPlayerNameLookup(context, records);

                results = CreateLeaderboardPage(records.ToArray(), playerNameLookup, recordTransformer, page);
            }

            results.Cache(queryString);
            return results;
        }

        public static PlayerRecord[] PaginateRecords(PlayerRecord[] records, int page)
        {
            return records.Skip(page * PageLimit)
                    .Take(PageLimit)
                    .ToArray();
        }

        public static bool TryGetCachedLeaderboardPage(string searchKey, out LeaderboardPage results)
        {
            results = null;
            if (PlayerRecordCache.CachedRecords.TryGetValue(searchKey, out var cachedResults))
            {
                if (cachedResults.RequeryTimestamp >= Intersect.Utilities.Timing.Global.MillisecondsUtc)
                {
                    Console.WriteLine("Returning cached result!");
                    results = cachedResults;
                }
            }

            return results != null;
        }

        private static PlayerRecord[] GetMatchingRecords(PlayerContext context, RecordType type, Guid recordId, RecordScoring scoreType)
        {
            if (context == null)
            {
                return Array.Empty<PlayerRecord>();
            }

            PlayerRecord[] queryRecords;

            if (scoreType == RecordScoring.High)
            {
                queryRecords = context.Player_Record
                    .Where(record => record.Type == type && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderByDescending(record => record.Amount)
                    .ToArray();
            }
            else
            {
                queryRecords = context.Player_Record
                    .Where(record => record.Type == type && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderBy(record => record.Amount)
                    .ToArray();
            }

            var idx = 0;
            foreach (var record in queryRecords)
            {
                record.Index = idx;
                idx++;
            }

            return queryRecords;
        }

        private static Dictionary<Guid, string> GetPlayerNameLookup(PlayerContext context, List<PlayerRecord> records)
        {
            Dictionary<Guid, string> playerNameLookup = new Dictionary<Guid, string>();
            if (context == null || records == null)
            {
                return playerNameLookup;
            }
            
            List<Guid> playersInRecords = new List<Guid>();
            foreach (var record in records)
            {
                playersInRecords.Add(record.PlayerId);
                var teammateIds = record.Teammates.Select(tm => tm.PlayerId);
                playersInRecords.AddRange(teammateIds);
            }
            playersInRecords = playersInRecords.Distinct().ToList();

            var relevantPlayers = context.Players
                .Where(player => playersInRecords.Contains(player.Id));
            foreach (var player in relevantPlayers)
            {
                playerNameLookup[player.Id] = player.Name;
            }

            return playerNameLookup;
        }

        private static LeaderboardPage CreateLeaderboardPage(PlayerRecord[] records, Dictionary<Guid, string> playerNameLookup, Func<long, string> recordTransformer, int currentPage)
        {
            List<RecordDto> dtos = new List<RecordDto>();
            foreach (var record in records)
            {
                List<string> names = new List<string>();
                if (record.Teammates.Count > 0)
                {
                    foreach (var teammate in record.Teammates)
                    {
                        names.Add(playerNameLookup[teammate.PlayerId]);
                    }
                }
                else
                {
                    names.Add(playerNameLookup[record.PlayerId]);
                }

                var name = string.Join(", ", names);
                string formattedRecord = recordTransformer(record.Amount);

                dtos.Add(new RecordDto(name, formattedRecord, record.Index));
            }

            return new LeaderboardPage(dtos, Intersect.Utilities.Timing.Global.MillisecondsUtc, currentPage);
        }
    }
}