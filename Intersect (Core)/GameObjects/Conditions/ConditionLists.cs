using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intersect.GameObjects.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intersect.GameObjects.Conditions
{

    [JsonConverter(typeof(ConditionListsSerializer))]
    public partial class ConditionLists
    {

        public List<ConditionList> Lists = new List<ConditionList>();

        public ConditionLists()
        {
        }

        public ConditionLists(string data)
        {
            Load(data);
        }

        public int Count => Lists?.Count ?? 0;

        public void Load(string data)
        {
            Lists.Clear();
            JsonConvert.PopulateObject(
                data, Lists,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                }
            );
        }

        public string Data()
        {
            return JsonConvert.SerializeObject(
                Lists,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
                }
            );
        }

        public List<string> ConditionListsToRequirementsString()
        {
            List<string> requirementLists = new List<string>();

            foreach (var conditionList in Lists)
            {
                var requirements = new List<string>();

                var classes = new List<string>();
                var classRanks = new List<string>();
                var stats = new List<string>();
                var equip = new List<string>();
                foreach (var condition in conditionList.Conditions)
                {
                    if (condition.Type == ConditionTypes.ClassIs && condition is ClassIsCondition classIs)
                    {
                        if (condition.Negated)
                        {
                            classes.Add($"NOT {ClassBase.GetName(classIs.ClassId)}");
                        }
                        else
                        {
                            classes.Add(ClassBase.GetName(classIs.ClassId));
                        }
                    }
                    
                    else if (condition.Type == ConditionTypes.LevelOrStat && condition is LevelOrStatCondition statCond)
                    {
                        if (condition.Negated)
                        {
                            classRanks.Add($"NOT {statCond.GetPrettyString()}");
                        }
                        else
                        {
                            stats.Add(statCond.GetPrettyString());
                        }
                    }
                    
                }

                if (classes.Count > 0)
                {
                    requirements.Add(string.Join(" and ", classes));
                }

                if (classRanks.Count > 0)
                {
                    requirements.Add(string.Join(", ", classRanks));
                }
                
                if (stats.Count > 0)
                {
                    requirements.Add(string.Join(", ", stats));
                }

                if (equip.Count > 0)
                {
                    requirements.Add(string.Join(", ", equip));
                }
                

                requirementLists.Add(string.Join("; ", requirements));
            }

            return requirementLists.Where(str => !string.IsNullOrWhiteSpace(str)).ToList();
        }

    }

    public partial class ConditionListsSerializer : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();
            var lists = existingValue != null ? (ConditionLists) existingValue : new ConditionLists();
            lists.Load((string) properties[0].Value);

            return lists;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Lists");
            serializer.Serialize(writer, ((ConditionLists) value).Data());
            writer.WriteEndObject();
        }

    }

}
