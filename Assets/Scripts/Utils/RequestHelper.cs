using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public enum RequestStatus
    {
        None,
        Done,
        Failed,
        InProgress
    }

    public class RequestHelper
    {
        public RequestStatus LastRequestStatus => _lastRequestStatus;
        private RequestStatus _lastRequestStatus = RequestStatus.None;

        private static RequestHelper _instance;

        // TODO:: Зарефакторить с DI контейнерами
        public static RequestHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RequestHelper();
                }

                return _instance;
            }
        }

        public void SendRequest(string url, string stringData, Action onSuccess = null, Action onFail = null)
        {
            CoroutineManager.Instance.StartCoroutine(SendRequestCoroutine(url, stringData, onSuccess, null));
        }

        public IEnumerator SendRequestCoroutine(string url, string stringData, Action onSuccess = null, Action onFail = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
            byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(stringData);
            webRequest.uploadHandler = new UploadHandlerRaw(encodedPayload);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("cache-control", "no-cache");

            _lastRequestStatus = RequestStatus.InProgress;
            yield return webRequest.SendWebRequest();


            if (webRequest.responseCode == 200)
            {
                onSuccess?.Invoke();
                _lastRequestStatus = RequestStatus.Done;
            }
            else
            {
                onFail?.Invoke();
                _lastRequestStatus = RequestStatus.Failed;
            }
            
        }
    }
}