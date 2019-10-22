using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayOnStartDelay : MonoBehaviour
{
    public DelayedEvent[] events;
    void Start()
    {
        foreach (DelayedEvent e in events)
        {
            StartCoroutine(IDelayInvoke(e));
        }
    }
    IEnumerator IDelayInvoke(DelayedEvent e)
    {
        yield return new WaitForSeconds(e.delayTime);
        e.e?.Invoke();
    }
}
[System.Serializable]
public class DelayedEvent
{
    public float delayTime;
    public UnityEvent e;
}
