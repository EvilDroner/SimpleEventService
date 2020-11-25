using Newtonsoft.Json;

namespace Analytics.EventDatas
{
    public class ChestOpenedEventData:IEventData
    {
        [JsonProperty("chest")] public int ChestID;
    }
}