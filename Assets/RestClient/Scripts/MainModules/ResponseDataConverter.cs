using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// unrem this for converting flatbuffer
//using System.Text;
//using System;
//using System.Reflection;
//using Google.FlatBuffers;

public class ResponseDataConverter : MonoBehaviour
{
    public static T ConvertJson<T>(byte[] rawData)
    {
        string jsonString = System.Text.Encoding.UTF8.GetString(rawData);
        return JsonUtility.FromJson<T>(jsonString);
    }

    // unrem this for converting flatbuffer
    //public static T ConvertFlatBuffer<T>(byte[] data)
    //{
    //    StringBuilder sb = new StringBuilder();

    //    Type type = typeof(T);
    //    string methodName = sb.Append("GetRootAs").Append(type.Name).ToString();

    //    MethodInfo method = type.GetMethod(methodName, new Type[] { typeof(ByteBuffer) });
    //    if (method == null)
    //    {
    //        return default;
    //    }

    //    if (data == null)
    //    {
    //        Debug.Log("No data for converting");
    //        return default;
    //    }

    //    ByteBuffer dataByteBuffer = new ByteBuffer(data);
    //    return (T)method.Invoke(null, new object[] { dataByteBuffer });
    //}
}
