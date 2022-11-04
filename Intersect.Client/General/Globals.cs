﻿using System;
using System.Collections.Generic;

using Intersect.Client.Entities;
using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.Database;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Input;
using Intersect.Client.Framework.Sys;
using Intersect.Client.Items;
using Intersect.Client.Plugins.Interfaces;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;

namespace Intersect.Client.General
{

    public static partial class Globals
    {

        //Only need 1 table, and that is the one we see at a given moment in time.
        public static CraftingTableBase ActiveCraftingTable;

        public static int AnimFrame = 0;

        //Bag
        public static Item[] Bag = null;

        //Bank
        public static Item[] Bank;
        public static bool GuildBank;
        public static int BankSlots;
        public static int BankValue;

        public static bool ConnectionLost;

        //Game Systems
        public static GameContentManager ContentManager;

        public static int CurrentMap = -1;

        public static GameDatabase Database;

        //Entities and stuff
        //public static List<Entity> Entities = new List<Entity>();
        public static Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();

        public static List<Guid> EntitiesToDispose = new List<Guid>();

        //Control Objects
        public static List<Dialog> EventDialogs = new List<Dialog>();

        public static Dictionary<Guid, Guid> EventHolds = new Dictionary<Guid, Guid>();

        //Game Lock
        public static object GameLock = new object();

        //Game Shop
        //Only need 1 shop, and that is the one we see at a given moment in time.
        public static ShopBase GameShop;

        //Game Quest Board
        //Only need 1 shop, and that is the one we see at a given moment in time.
        public static QuestBoardBase QuestBoard;

        public static Dictionary<Guid, bool> QuestBoardRequirements;

        //Crucial game variables

        internal static List<IClientLifecycleHelper> ClientLifecycleHelpers { get; } =
            new List<IClientLifecycleHelper>();

        internal static void OnLifecycleChangeState()
        {
            ClientLifecycleHelpers.ForEach(
                clientLifecycleHelper => clientLifecycleHelper?.OnLifecycleChangeState(GameState)
            );
        }

        internal static void OnGameUpdate()
        {
            ClientLifecycleHelpers.ForEach(
                clientLifecycleHelper => clientLifecycleHelper?.OnGameUpdate(GameState)
            );
        }

        private static GameStates mGameState = GameStates.Intro;

        /// <see cref="GameStates" />
        public static GameStates GameState
        {
            get => mGameState;
            set
            {
                mGameState = value;
                OnLifecycleChangeState();
            }
        }

        public static List<Guid> GridMaps = new List<Guid>();

        public static bool HasGameData = false;

        public static bool InBag = false;

        public static bool InBank = false;
        
        public static bool InQuestBoard = false;

        //Crafting station
        public static bool InCraft = false;

        public static bool InShop => GameShop != null;

        public static bool InTrade = false;

        public static bool CanCloseInventory => !(InBag || InBank || InCraft || InShop || InTrade);

        public static GameInput InputManager;

        public static bool IntroComing = true;

        public static long IntroDelay = 3200;

        //Engine Progression
        public static int IntroIndex = 0;

        public static long IntroStartTime = -1;

        public static bool IsRunning = true;

        public static bool JoiningGame = false;

        public static bool LoggedIn = false;

        //Map/Chunk Array
        public static Guid[,] MapGrid;
        public static Dictionary<Guid, string> GridNames;

        public static long MapGridHeight;

        public static long MapGridWidth;

        //Local player information
        public static Player Me;

        public static bool InitialFade = false;

        public static bool MoveRouteActive = false;

        public static bool NeedsMaps = true;

        //Event Guid and the Map its associated with
        public static Dictionary<Guid, Dictionary<Guid, EventEntityPacket>> PendingEvents =
            new Dictionary<Guid, Dictionary<Guid, EventEntityPacket>>();

        //Event Show Pictures
        public static ShowPicturePacket Picture;

        public static List<Guid> QuestOffers = new List<Guid>();

        public static int QuestOfferIndex = 0; // Index of current quest offer we're looking at

        public static Random Random = new Random();

        public static GameSystem System;

        //Trading (Only 2 people can trade at once)
        public static Item[,] Trade;

        //Scene management
        public static bool WaitingOnServer = false;

        public static bool WaitingOnServerDispose = false;

        public static bool InMapTransition = false;

        // Used for when a warp is using a transition - we want to transition them AFTER the client has "faded out"
        public static Guid futureWarpMapId;

        public static float futureWarpX;

        public static float futureWarpY;

        public static byte futureWarpDir;

        public static MapInstanceType futureWarpInstanceType = MapInstanceType.NoChange;

        public static Entity GetEntity(Guid id, EntityTypes type)
        {
            if (Entities.ContainsKey(id))
            {
                if (Entities[id].GetEntityType() == type)
                {
                    EntitiesToDispose.Remove(Entities[id].Id);

                    return Entities[id];
                }

                Entities[id].Dispose();
                Entities.Remove(id);
            }

            return null;
        }

    }

    public static partial class Globals
    {
        public static bool TradeAccepted { get; set; }
        public static bool WaitFade { get; set; }
        public static int IntroHFrame { get; set; }
        public static int IntroVFrame { get; set; }
        public static bool AnimatedIntro { get; set; }
        public static int IntroFps { get; set; } = 10;
        public static int IntroHFrames { get; set; }
        public static int IntroVFrames { get; set; }
        public static long IntroUpdateTime { get; set; }
        public static bool StartMenuMusic { get; set; }
        public static long IntroBlackDelay { get; set; } = 2500;
        public static bool JinglePlayed { get; set; }
    }
}
