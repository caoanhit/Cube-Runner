using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
        LoadData();
        score.SetValue(0);
        coin.SetValue(0);
        perfect.SetValue(0);
        maxPerfect.SetValue(0);
    }

    public IntVariable score;
    public IntVariable coin;
    public IntVariable perfect;
    public IntVariable highScore;
    public IntVariable ownedCoin;
    public IntVariable maxPerfect;
    public IntVariable maxPerfectRecorded;
    private int checkerId;
    private int currentPerfectCount;
    private void OnEnable()
    {
        Checker.OnPerfect += Perfect;
        Coin.OnCoinCollect += AddCoin;
    }
    private void OnDisable()
    {
        Coin.OnCoinCollect -= AddCoin;
    }
    private void AddCoin(int value)
    {
        ownedCoin.ApplyChange(value);
        coin.ApplyChange(value);
    }
    public void Perfect(int number)
    {
        perfect.ApplyChange(1);
        if (checkerId == number)
        {
            currentPerfectCount++;
            if (currentPerfectCount > maxPerfect)
            {
                maxPerfect.SetValue(currentPerfectCount);
            }
            checkerId++;
        }
        else
        {
            currentPerfectCount = 1;
            if (currentPerfectCount > maxPerfect)
                maxPerfect.SetValue(currentPerfectCount);
            checkerId = number + 1;
        }
    }
    public void SaveData()
    {
        ScoreData data = new ScoreData();
        if (score > highScore)
        {
            data.highScore = score;
            highScore.SetValue(score);
        }
        if (maxPerfect > maxPerfectRecorded)
        {
            data.maxPerfectRecorded = maxPerfect;
            maxPerfectRecorded.SetValue(maxPerfect);
        }
        data.coinCount = ownedCoin;
        SaveLoad.SaveScore(data);
    }
    public void LoadData()
    {
        ScoreData data = SaveLoad.LoadScore();
        highScore.SetValue(data.highScore);
        ownedCoin.SetValue(data.coinCount);
        maxPerfectRecorded.SetValue(data.maxPerfectRecorded);
    }
}
