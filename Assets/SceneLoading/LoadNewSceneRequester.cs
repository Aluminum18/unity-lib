using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewSceneRequester : MonoBehaviour
{
    [SerializeField]
    private string _nextScene;

    public void LoadNextScene()
    {
        ChangeSceneRequester.Instance.ChangeScene(_nextScene);
    }
}
