using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem;
using UnityEngine;

namespace Analytics.EventServiceCore
{
    [Serializable]
    public struct RawEvent
    {
        [JsonProperty("type")] public string Type;
        [JsonProperty("data")] public string Data;
    }

    [Serializable]
    public class EventCollection 
    {
        [JsonIgnore] public int EventsCount => _events.Count;
        [JsonProperty("events")] private Queue<RawEvent> _events = new Queue<RawEvent>();

        public void AddEventToQueue(string type, string data)
        {
            _events.Enqueue(new RawEvent() {Type = type, Data = data});
        }

        public void RemoveFirstNElements(int elemCount)
        {
            if (elemCount > _events.Count)
            {
                Debug.LogError($"[EventService] Trying to remove {elemCount} saved events, but storage have only {_events.Count}!");
                return;
            }

            for (int i = 0; i < elemCount; i++)
            {
                _events.Dequeue();
            }
        }
    }
}