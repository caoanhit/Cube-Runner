using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnControl : MonoBehaviour
{
    public IntVariable score;
    public UnityEvent OnRespawnAvailable;
    bool respawned;

    public void Respawn()
    {
        if (!respawned && score > 100)
        {
            respawned = true;
            OnRespawnAvailable.Invoke();
        }
    }
}
