using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnControl : MonoBehaviour
{
    public UnityEvent OnRespawnAvailable;
    bool respawned;

    public void Respawn()
    {
        if (!respawned)
        {
            respawned = true;
            OnRespawnAvailable.Invoke();
        }
    }
}
