using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.Text;

public class RequestSender : MonoBehaviour
{
    public delegate void SendApiSuccessHandler<T>(T response);
    public delegate void SendApiSuccessHandler();
    public delegate void SendApiFailedHandler();
    public delegate void SendApiFailedByNetWorkHandler();

    private enum RequestType
    {
        GET,
        POST,

        // not used yet
        PUT,
        DELETE
    }

    /// <typeparam name="T">Type of response</typeparam>
    public void SendGetApi<T>(string path,
                                SendApiSuccessHandler<T> successCallback,
                                SendApiFailedHandler failedCallback,
                                SendApiFailedByNetWorkHandler networkErrorCallback,
                                string token = null,
                                params Tuple<string, string>[] customHeader)
    {
        StartCoroutine(SendApiCor(path, RequestType.GET, successCallback, failedCallback, networkErrorCallback, null, token, customHeader));
    }


    /// <summary>
    /// Used this method when response data is not used
    /// </summary>
    public void SendPostApi(string path,
                                SendApiSuccessHandler successCallback,
                                SendApiFailedHandler failedCallback,
                                SendApiFailedByNetWorkHandler networkErrorCallback,
                                object data = null,
                                string token = null,
                                params Tuple<string, string>[] customHeader)
    {
        string requestBody = JsonUtility.ToJson(data);
        StartCoroutine(SendApiCor(path, RequestType.POST, successCallback, failedCallback, networkErrorCallback, requestBody, token, customHeader));
    }

    /// <typeparam name="T">Type of response</typeparam>
    public void SendPostApi<T>(string path,
                                SendApiSuccessHandler<T> successCallback,
                                SendApiFailedHandler failedCallback,
                                SendApiFailedByNetWorkHandler networkErrorCallback,
                                string data = null,
                                string token = null,
                                params Tuple<string, string>[] customHeader)
    {
        StartCoroutine(SendApiCor(path, RequestType.POST, successCallback, failedCallback, networkErrorCallback, data, token, customHeader));
    }

    /// <typeparam name="T">Type of response</typeparam>
    public void SendPostApi<T>(string path,
                                SendApiSuccessHandler<T> successCallback,
                                SendApiFailedHandler failedCallback,
                                SendApiFailedByNetWorkHandler networkErrorCallback,
                                object data = null,
                                string token = null,
                                params Tuple<string, string>[] customHeader)
    {
        string requestBodyString = JsonUtility.ToJson(data);
        SendPostApi(path, successCallback, failedCallback, networkErrorCallback, requestBodyString, token, customHeader);
    }

    private IEnumerator SendApiCor<T>(string path,
                                RequestType requestType,
                                SendApiSuccessHandler<T> successCallback,
                                SendApiFailedHandler failedCallback,
                                SendApiFailedByNetWorkHandler networkErrorCallback,
                                string data,
                                string token,
                                Tuple<string, string>[] customHeader)
    {
        using (var request = GetUnityWebRequest(path, requestType, data, token, customHeader))
        {
            if (request == null)
            {
                yield break;
            }

            yield return request.SendWebRequest();

            if (request.isHttpError)
            {
                failedCallback?.Invoke();
                yield break;
            }
            if (request.isNetworkError)
            {
                networkErrorCallback?.Invoke();
                yield break;
            }

            var response = JsonUtility.FromJson<T>(request.downloadHandler.text);
            successCallback?.Invoke(response);
        }
    }

    private IEnumerator SendApiCor(string path,
                            RequestType requestType,
                            SendApiSuccessHandler successCallback,
                            SendApiFailedHandler failedCallback,
                            SendApiFailedByNetWorkHandler networkErrorCallback,
                            string data,
                            string token,
                            Tuple<string, string>[] customHeader)
    {
        using (var request = GetUnityWebRequest(path, requestType, data, token, customHeader))
        {
            if (request == null)
            {
                yield break;
            }
            yield return request.SendWebRequest();

            if (request.isHttpError)
            {
                failedCallback?.Invoke();
                yield break;
            }
            if (request.isNetworkError)
            {
                networkErrorCallback?.Invoke();
                yield break;
            }
            successCallback?.Invoke();
        }
    }

    private UnityWebRequest GetUnityWebRequest(string url, RequestType requestType, string data, string token, params Tuple<string, string>[] customHeader)
    {
        switch (requestType)
        {
            case RequestType.GET:
                {
                    return CreateGetRequest(url, token, customHeader);
                }
            case RequestType.POST:
                {
                    return CreatePostRequest(url, data, token, customHeader);
                }
            default:
                {
                    return null;
                }
        }
    }

    private UnityWebRequest CreateGetRequest(string url, string token, params Tuple<string, string>[] customHeader)
    {
        var request = UnityWebRequest.Get(url);

        if (token != null)
        {
            StringBuilder tokenSb = new StringBuilder("Bearer ");
            tokenSb.Append(token);
            request.SetRequestHeader("Authorization", tokenSb.ToString());
        }

        if (customHeader.Length != 0)
        {
            for (int i = 0; i < customHeader.Length - 1; i++)
            {
                var headerField = customHeader[i];
                request.SetRequestHeader(headerField.Item1, headerField.Item2);
            }
        }

        return request;
    }

    private UnityWebRequest CreatePostRequest(string url, string requestBody, string token, params Tuple<string, string>[] customHeader)
    {
        UnityWebRequest request = new UnityWebRequest
        {
            url = url,
            method = "POST"
        };

        request.downloadHandler = new DownloadHandlerBuffer();

        if (requestBody != null)
        {
            byte[] rawData = new UTF8Encoding().GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(rawData);
        }

        if (token != null)
        {
            request.SetRequestHeader("Authorization", token);
        }

        if (customHeader.Length != 0)
        {
            for (int i = 0; i < customHeader.Length; i++)
            {
                var headerField = customHeader[i];
                request.SetRequestHeader(headerField.Item1, headerField.Item2);
            }
        }

        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }
}
