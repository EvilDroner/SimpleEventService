using UnityEngine;

namespace Analytics.EventServiceCore
{
    [CreateAssetMenu(fileName = "Config", menuName = "Analytics/EventService", order = 1)]
    public class EventServiceConfig : ScriptableObject, IEventServiceConfig
    {
        [SerializeField] private string _serverIP;
        [SerializeField] private float _sendDelay;
        

        public string ServerIP => _serverIP;
        public float SendDelay => _sendDelay;
    }
}