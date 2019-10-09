using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void SaveScore(ScoreData data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }
    public static ScoreData LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + "/score.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Open);
            ScoreData data = bf.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;
        }
        return new ScoreData();
    }
    public static void SaveUnlockData(UnlockData data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/unlock.sav", FileMode.Create);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }
    public static UnlockData LoadUnlockData()
    {
        if (File.Exists(Application.persistentDataPath + "/unlock.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/unlock.sav", FileMode.Open);
            UnlockData data = bf.Deserialize(stream) as UnlockData;
            stream.Close();
            return data;
        }
        return new UnlockData();
    }
}
[System.Serializable]
public class ScoreData
{
    public int highScore;
    public int coinCount;
}

[System.Serializable]
public class UnlockData
{
    public bool[] worlds;
    public bool[] chars;
    public bool[] items;
    public UnlockData()
    {
        worlds = new bool[100];
        worlds[0] = true;
        worlds[1] = true;
        chars = new bool[100];
        chars[0] = true;
        chars[1] = true;
        items = new bool[100];
    }
}