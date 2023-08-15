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
        if (GUILayout.Button("Broadcast message by name"))
        {
            var myTarget = (SOMessageBroadcaster)target;
            myTarget.EditorOnly_BroadcastMessageByName(myTarget._editorOnly_messageToBroadcast);
        }
        DrawDefaultInspector();
    }
}
