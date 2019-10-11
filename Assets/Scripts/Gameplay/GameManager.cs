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
    public GameEvent OnGameStart, OnGameStop, OnPause, OnUnpause, OnRetry;
    bool gameStarted;
    bool gamePaused;
    private void Start()
    {
        if (retryTrigger == 1)
        {
            OnRetry.Raise();
            retryTrigger.SetValue(0);
        }
    }
    void Update()
    {
        if (TouchInput.Instance.tap && !gameStarted)
        {
            GameStart();
        }
    }
    public void GameStart()
    {
        gameStarted = true;
        OnGameStart?.Raise();
    }
    public void GameStop()
    {
        gameStarted = false;
        OnGameStop?.Raise();
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
