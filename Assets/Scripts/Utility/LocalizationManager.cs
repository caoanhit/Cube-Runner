using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, string> localizedText;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLocalizedText(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            localizedText = new Dictionary<string, string>();
            string dataJson = File.ReadAllText(filePath);
            LocalizationData localizedData = JsonUtility.FromJson<LocalizationData>(dataJson);
            for (int i = 0; i < localizedData.items.Length; i++)
            {
                localizedText.Add(localizedData.items[i].key, localizedData.items[i].value);
            }
        }
        else
        {
            Debug.Log("Cannot file file");
        }
    }
    public void SetLanguage(string language)
    {
        LoadLocalizedText("localizedText_" + language + ".json");
    }
    public string LoadLanguageSetting()
    {
        string language = PlayerPrefs.GetString("lang", "nosetting");
        if (language == "nosetting")
            return LanguageToString(Application.systemLanguage);
        return language;
    }
    public void SaveLanguageSetting(string language)
    {
        PlayerPrefs.SetString("lang", language);
    }
    public void UpdateText()
    {

    }
    public string LanguageToString(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.English: return "en";
            case SystemLanguage.French: return "fr";
            case SystemLanguage.Japanese: return "jp";
            case SystemLanguage.Vietnamese: return "vi";
            default: return "en";
        }
    }
}
[System.Serializable]
public class LocalizedItem
{
    public string key;
    public string value;
}
public class LocalizationData
{
    public LocalizedItem[] items;
}