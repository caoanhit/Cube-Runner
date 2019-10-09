using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldElement : MonoBehaviour
{
    public WorldType type;
    void Start()
    {
        WorldManager.instance.Assign(type, this.gameObject);
        if (WorldManager.instance.currentType != type) this.gameObject.SetActive(false);
    }
}
