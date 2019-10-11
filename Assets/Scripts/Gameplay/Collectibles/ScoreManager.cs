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
    }
    public IntVariable score;
    public IntVariable coin;
    public IntVariable highScore;
    public IntVariable ownedCoin;
    private void OnEnable()
    {
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
    public void SaveData()
    {
        ScoreData data = new ScoreData();
        if (score > highScore)
        {
            data.highScore = score;
            highScore.SetValue(score);
        }
        data.coinCount = ownedCoin;
        SaveLoad.SaveScore(data);
    }
    public void LoadData()
    {
        ScoreData data = SaveLoad.LoadScore();
        highScore.SetValue(data.highScore);
        ownedCoin.SetValue(data.coinCount);

    }
}
