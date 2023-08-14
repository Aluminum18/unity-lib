using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SOMessageBroadcaster))]
public class SOMessageBroadcasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Broadcast all messages"))
        {
            var myTarget = (SOMessageBroadcaster)target;
            myTarget.EditorOnly_BroadcastAllMessage();
        }
        DrawDefaultInspector();
    }
}
