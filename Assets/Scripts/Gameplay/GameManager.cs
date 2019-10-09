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
    public GameEvent OnGameStart, OnGameStop, OnPause, OnUnpause;
    bool gameStarted = false;
    bool gamePaused = false;
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
        gamePaused=true;
        OnPause?.Raise();
    }
    public void Unpause()
    {
        gamePaused=false;
        OnUnpause?.Raise();
    }
    public void Restart()
    {
        Transition.Instance.ReloadScene();
    }
}
