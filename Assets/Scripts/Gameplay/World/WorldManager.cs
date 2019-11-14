using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum WorldType { Forest, Savana, Snow, Mountain }
public class WorldManager : MonoBehaviour
{
    public event Action<WorldType, float> OnWorldTransition;
    public static WorldManager instance;
    public float transitionSpeed;
    public WorldType currentType = WorldType.Forest;
    public int[] prices;
    private WorldType previousType = WorldType.Forest;
    [Range(0, 1)]
    public float transition;
    public GameObject nextButton;
    public GameObject previousButton;
    public WorldDisplay worldDisplay;
    private Dictionary<WorldType, List<GameObject>> worldElements;
    private bool[] unlocked;
    bool inTransition;
    bool updated;
    float defaultFov;
    private void Awake()
    {
        worldElements = new Dictionary<WorldType, List<GameObject>>();
        instance = this;
        foreach (int i in Enum.GetValues(typeof(WorldType)))
        {
            List<GameObject> objList = new List<GameObject>();
            worldElements.Add((WorldType)i, objList);
        }
        References reference = SaveLoad.LoadReferences();
        UnlockData unlockData = SaveLoad.LoadUnlockData();
        unlocked = unlockData.worlds;
        WorldType type = (WorldType)reference.world;
        worldDisplay.SetData(type.ToString(), prices[(int)type], unlocked[(int)type]);
        SetWorld(type);
        if ((int)type <= 0) previousButton.SetActive(false);
        if ((int)type >= Enum.GetNames(typeof(WorldType)).Length - 1) nextButton.SetActive(false);
    }
    private void Update()
    {
        if (transition < 1)
        {
            OnWorldTransition?.Invoke(currentType, transition);
            float alpha = Mathf.Clamp(Mathf.Abs((transition - 0.5f) * 2), 0, 1);
            worldDisplay.SetAlpha(alpha);
            if (transition >= 0.5f && !updated)
            {
                worldDisplay.SetData(currentType.ToString(), prices[(int)currentType], unlocked[(int)currentType]);
                updated = true;
            }
            transition += Time.deltaTime * transitionSpeed;

        }
        else if (inTransition)
        {
            transition = 1;
            worldDisplay.SetAlpha(1);
            DisableWorld(previousType);
            inTransition = false;
        }
    }
    public void SwitchWorld(WorldType type)
    {
        if (type != currentType)
        {
            previousType = currentType;
            currentType = type;
            inTransition = true;
            OnWorldTransition?.Invoke(currentType, 0);
            EnableWorld(type);
            transition = 0;
        }
    }
    public void EnableWorld(WorldType type)
    {
        foreach (GameObject obj in worldElements[type])
        {
            obj.SetActive(true);
        }
    }
    public void DisableAllWorldsExept(params WorldType[] types)
    {
        foreach (int i in Enum.GetValues(typeof(WorldType)))
        {
            if (!Array.Exists(types, type => type == (WorldType)i))
            {
                DisableWorld((WorldType)i);
            }
        }
    }
    public void SetWorld(WorldType type)
    {
        previousType = currentType;
        currentType = type;
        OnWorldTransition?.Invoke(currentType, 1);
        DisableAllWorldsExept(type);
    }
    public void DisableWorld(WorldType type)
    {
        foreach (GameObject obj in worldElements[type])
        {
            obj.SetActive(false);
        }
    }
    public void Assign(WorldType type, GameObject obj)
    {
        worldElements[type].Add(obj);
    }
    [ContextMenu("Next")]
    public void NextWorld()
    {
        if (!inTransition)
        {
            int i = (int)currentType + 1;
            if (Enum.IsDefined(typeof(WorldType), i))
            {
                previousButton.SetActive(true);
                SwitchWorld((WorldType)i);
                updated = false;
            }
            if (i >= Enum.GetNames(typeof(WorldType)).Length - 1) nextButton.SetActive(false);
        }
    }
    [ContextMenu("Previous")]
    public void PreviousWorld()
    {
        if (!inTransition)
        {
            int i = (int)currentType - 1;
            if (Enum.IsDefined(typeof(WorldType), i))
            {
                SwitchWorld((WorldType)i);
                nextButton.SetActive(true);
                updated = false;
            }
            if (i <= 0) previousButton.SetActive(false);
        }
    }
    public void SaveWorldSetting()
    {
        References references = SaveLoad.LoadReferences();
        references.world = (int)currentType;
        SaveLoad.SaveReferences(references);
    }
    public void UnLockWorld()
    {
        if (ScoreManager.Instance.RemoveCoin(prices[(int)currentType]))
        {
            UnlockData unlockData = SaveLoad.LoadUnlockData();
            unlockData.worlds[(int)currentType] = true;
            worldDisplay.SetData(currentType.ToString(), 0, true);
        }
    }
}