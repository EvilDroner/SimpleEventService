using System.Collections;
using Analytics.EventDatas;
using Newtonsoft.Json;
using SaveSystem;
using UnityEngine;
using Utils;

namespace Analytics.EventServiceCore
{
    public class EventService : IEventService, ISaveable<EventCollection>
    {
        public string SaveId => "EventCollection";

        private IEventServiceConfig _config;
        private EventCollection _eventCollection = new EventCollection();

        private static EventService _instance;

        // TODO:: Зарефакторить с DI контейнерами
        public static EventService Instance
        {
            get
            {
                if (_instance == null)
                {
                    IEventServiceConfig config =
                        Resources.Load<EventServiceConfig>("Analytics/Config/EventServiceConfig");
                    _instance = new EventService(config);
                }

                return _instance;
            }
        }

        public EventService(IEventServiceConfig config)
        {
            _config = config;

            LoadSavedEvents(SaveManager.Instance.LoadSave<EventCollection>(SaveId));
            CoroutineManager.Instance.StartCoroutine(SendRequestCycle());
        }

        public void RegisterAction<T>(EventType action, T data) where T : IEventData
        {
            _eventCollection.AddEventToQueue(action.ToString(), JsonConvert.SerializeObject(data));
            SaveManager.Instance.Save(this);
        }

        public EventCollection Save()
        {
            return _eventCollection;
        }

        private IEnumerator SendRequestCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(_config.SendDelay);
                yield return new WaitUntil(() => RequestHelper.Instance.LastRequestStatus != RequestStatus.InProgress);
                int currentEventsCounts = _eventCollection.EventsCount;
                if (_eventCollection.EventsCount > 0)
                {
                    RequestHelper.Instance.SendRequest(
                        _config.ServerIP,
                        JsonConvert.SerializeObject(_eventCollection),
                        () =>
                        {
                            _eventCollection.RemoveFirstNElements(currentEventsCounts);
                            SaveManager.Instance.Save(this);
                        });
                }
            }
        }

        private void LoadSavedEvents(EventCollection collection)
        {
            if (collection == null)
            {
                return;
            }

            _eventCollection = collection;
        }
    }
}