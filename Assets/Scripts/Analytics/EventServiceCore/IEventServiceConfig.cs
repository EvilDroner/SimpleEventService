namespace Analytics.EventServiceCore
{
    public interface IEventServiceConfig
    {
        string ServerIP { get; }
        float SendDelay { get; }
    }
}