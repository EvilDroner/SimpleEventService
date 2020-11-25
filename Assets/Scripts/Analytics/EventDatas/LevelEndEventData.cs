using Newtonsoft.Json;

namespace Analytics.EventDatas
{
    public struct LevelEndEventData : IEventData
    {
        [JsonProperty("level")] public int Level;
    }
}