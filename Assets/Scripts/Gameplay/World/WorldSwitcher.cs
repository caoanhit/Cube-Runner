﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public WorldManager worldManager;
    private void Update()
    {
        if (TouchInput.Instance.swipeLeft) WorldManager.instance.NextWorld();
        if (TouchInput.Instance.swipeRight) WorldManager.instance.PreviousWorld();
    }
}
