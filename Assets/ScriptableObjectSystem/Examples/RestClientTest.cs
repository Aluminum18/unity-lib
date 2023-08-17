using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class RestClientTest : MonoBehaviour
//{
//    public async UniTaskVoid DoSomeRequests()
//    {
//        string url = "request url";
//        string tokenIfAny = "sample token";
//        Tuple<string, string> headerIfAny = new("header1", "headerValue1");

//        var getResponseData = await RequestSenderAsync.SendGetRequest(url, tokenIfAny, headerIfAny);
//        JsonResponse yourGetResponse = ResponseDataConverter.ConvertJson<JsonResponse>(getResponseData.rawdata);
//        // Convert logic use built-in JsonUtility so your JsonResponse must be a Serialized class/struct

//        byte[] postData = new byte[1024];
//        var postResponse = await RequestSenderAsync.SendPostRequest(url, postData, tokenIfAny, headerIfAny);
//        FlatBufferResponse yourPostResponse = ResponseDataConverter.ConvertFlatBuffer<FlatBufferResponse>(postResponse.rawdata);
//        // Your FlatBufferResponse must be a struct implements IFlatbufferObject
//    }
//}

public class JsonResponse
{

}

public class FlatBufferResponse
{

}
