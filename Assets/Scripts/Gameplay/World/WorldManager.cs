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
    private WorldType previousType = WorldType.Forest;
    [Range(0, 1)]
    public float transition;
    private Dictionary<WorldType, List<GameObject>> worldElements;
    bool inTransition;
    private void Awake()
    {
        worldElements = new Dictionary<WorldType, List<GameObject>>();
        instance = this;
        foreach (int i in Enum.GetValues(typeof(WorldType)))
        {
            List<GameObject> objList = new List<GameObject>();
            worldElements.Add((WorldType)i, objList);
        }
        OnWorldTransition.Invoke(currentType, 1);
    }
    private void Update()
    {
        if (transition < 1)
        {
            OnWorldTransition(currentType, transition);
            transition += Time.deltaTime * transitionSpeed;
        }
        else if (inTransition)
        {
            transition = 1;
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
            OnWorldTransition.Invoke(currentType, 0);
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
    public void DisableAllWorldExept(params WorldType[] types)
    {
        foreach (int i in Enum.GetValues(typeof(WorldType)))
        {
            if (!Array.Exists(types, type => type == (WorldType)i))
            {
                DisableWorld((WorldType)i);
            }
        }
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
    public void NextWorld()
    {
        int i = (int)currentType + 1;
        if (Enum.IsDefined(typeof(WorldType), i))
        {
            SwitchWorld((WorldType)i);
        }
        else SwitchWorld((WorldType)0);
    }
    public void PreviousWorld()
    {
        int i = (int)currentType - 1;
        if (Enum.IsDefined(typeof(WorldType), i))
        {
            SwitchWorld((WorldType)i);
        }
        else SwitchWorld((WorldType)(Enum.GetNames(typeof(WorldType)).Length - 1));
    }
    public void SaveWorldSetting()
    {

    }
    public void UnLockWorld()
    {

    }
}
