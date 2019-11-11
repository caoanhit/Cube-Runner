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
        combo.SetValue(0);
    }

    public IntVariable score;
    public IntVariable coin;
    public IntVariable perfect;
    public IntVariable highScore;
    public IntVariable ownedCoin;
    public IntVariable combo;
    public IntVariable maxCombo;
    private int checkerId;
    private int currentPerfectCount;
    private ScoreData data;
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
    public void RemoveCoin(int amount)
    {
        data.coinCount -= amount;
        SaveLoad.SaveScore(data);
    }
    public void Perfect(int number)
    {
        perfect.ApplyChange(1);
        if (checkerId == number)
        {
            currentPerfectCount++;
            if (currentPerfectCount > combo)
            {
                combo.SetValue(currentPerfectCount);
            }
            checkerId++;
        }
        else
        {
            currentPerfectCount = 1;
            combo.SetValue(currentPerfectCount);
            checkerId = number + 1;
        }
    }
    public void SaveData()
    {
        if (score > highScore)
        {
            data.highScore = score;
            highScore.SetValue(score);
        }
        if (combo > maxCombo)
        {
            data.maxCombo = combo;
            maxCombo.SetValue(combo);
        }
        data.coinCount = ownedCoin;
        SaveLoad.SaveScore(data);
    }
    public void LoadData()
    {
        data = SaveLoad.LoadScore();
        highScore.SetValue(data.highScore);
        ownedCoin.SetValue(data.coinCount);
        maxCombo.SetValue(data.maxCombo);
    }
}
