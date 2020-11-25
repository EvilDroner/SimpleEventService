using Analytics.EventDatas;
using Analytics.EventServiceCore;
using UnityEngine;
using EventType = Analytics.EventType;

public class SimpleEventGenerator : MonoBehaviour
{
    private void Start()
    {
        EventService.Instance.RegisterAction(EventType.LevelStart, new LevelStartEventData{Level = 1});
        EventService.Instance.RegisterAction(EventType.LevelComplete, new LevelEndEventData(){Level = 2});
        EventService.Instance.RegisterAction(EventType.ChestOpened, new ChestOpenedEventData(){ChestID = 5});
    }
}