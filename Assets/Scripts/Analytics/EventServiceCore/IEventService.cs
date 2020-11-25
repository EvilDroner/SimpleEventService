using Analytics.EventDatas;

namespace Analytics.EventServiceCore
{
    public interface IEventService
    {
        void RegisterAction<T>(EventType action, T data) where T : IEventData;
    }
}