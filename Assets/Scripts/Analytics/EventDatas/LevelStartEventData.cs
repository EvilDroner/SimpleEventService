using Newtonsoft.Json;

namespace Analytics.EventDatas
{
    public struct LevelStartEventData : IEventData
    {
        [JsonProperty("level")] public int Level;
    }
}