using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }
    public IntVariable retryTrigger;
    public GameEvent OnPause, OnUnpause;
    public UnityEvent OnRetry;
    public UnityEvent OnStart;
    bool gameStarted;
    bool gamePaused;
    private void Start()
    {
        if (retryTrigger == 1)
        {
            OnRetry?.Invoke();
            retryTrigger.SetValue(0);
        }
        else
        {
            OnStart?.Invoke();
        }
    }
    public void Pause()
    {
        gamePaused = true;
        OnPause?.Raise();
    }
    public void Unpause()
    {
        if (gamePaused)
        {
            gamePaused = false;
            OnUnpause?.Raise();
        }
    }
    public void Restart()
    {
        Transition.Instance.ReloadScene();
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            Pause();
        }
    }
    public void Retry()
    {
        retryTrigger.SetValue(1);
        Restart();
    }
}
