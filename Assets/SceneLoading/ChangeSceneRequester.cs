using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChangeSceneRequester : MonoSingleton<ChangeSceneRequester>
{
    [SerializeField]
    private UnityEvent _onStartCloseScene;
    [SerializeField]
    private UnityEvent _onClosedScene;
    [SerializeField]
    private UnityEvent _onStartOpenScene;
    [SerializeField]
    private UnityEvent _onOpenedScene;

    [Header("Config")]
    [SerializeField]
    private List<SceneTransitionObject> _transitionObjects;
    [SerializeField]
    private float _closeSceneDuration;
    [SerializeField]
    private float _openSceneDuration;

    private string _nextSceneName;

    public void ChangeScene(string sceneName)
    {
        _nextSceneName = sceneName;
        if (_transitionObjects.Count == 0)
        {
            Debug.LogWarning("No transition objects found, load with no transition!");
            LoadScene();
            return;
        }

        for (int i = 0; i < _transitionObjects.Count; i++)
        {
            var transitionObj = _transitionObjects[i];
            transitionObj.DoCloseSceneAnim(_closeSceneDuration);
        }
    }

    private void LoadScene()
    {
        if (string.IsNullOrEmpty(_nextSceneName))
        {
            return;
        }

        SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);    
    }

    private void OpenTransition(Scene newScene, LoadSceneMode loadMode)
    {
        if (_transitionObjects.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _transitionObjects.Count; i++)
        {
            var transitionObj = _transitionObjects[i];
            transitionObj.DoOpenSceneAnim(_openSceneDuration);
        }
    }

    protected override void DoOnAwake()
    {
        base.DoOnAwake();
        SceneTransitionObject.OnStartCloseAnim += CheckAllTransitionsStartClose;
        SceneTransitionObject.OnFinishCloseAnim += CheckAllTransitionsClosed;
        SceneTransitionObject.OnStartOpenAnim += CheckAllTransitionsStartOpen;
        SceneTransitionObject.OnFinishOpenAnim += CheckAllTransitionsOpened;
        SceneManager.sceneLoaded += OpenTransition;
    }

    protected override void DoOnDestroy()
    {
        base.DoOnDestroy();
        SceneTransitionObject.OnStartCloseAnim -= CheckAllTransitionsStartClose;
        SceneTransitionObject.OnFinishCloseAnim -= CheckAllTransitionsClosed;
        SceneTransitionObject.OnStartOpenAnim -= CheckAllTransitionsStartOpen;
        SceneTransitionObject.OnFinishOpenAnim -= CheckAllTransitionsOpened;
        SceneManager.sceneLoaded -= OpenTransition;

    }

    int startClose = 0;
    private void CheckAllTransitionsStartClose()
    {
        startClose++;
        if (startClose == _transitionObjects.Count)
        {
            startClose = 0;
            _onStartCloseScene.Invoke();
        }
    }

    int closed = 0;
    private void CheckAllTransitionsClosed()
    {
        closed++;
        if (closed == _transitionObjects.Count)
        {
            closed = 0;
            LoadScene();
            _onClosedScene.Invoke();
        }
    }

    int startedOpen = 0;
    private void CheckAllTransitionsStartOpen()
    {
        startedOpen++;
        if (startedOpen == _transitionObjects.Count)
        {
            startedOpen = 0;
            _onStartOpenScene.Invoke();
        }
    }

    int opened = 0;
    private void CheckAllTransitionsOpened()
    {
        opened++;
        if (opened == _transitionObjects.Count)
        {
            opened = 0;
            _onOpenedScene.Invoke();
        }
    }
}
