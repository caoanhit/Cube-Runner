using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public static event Action<int> OnCoinCollect;
    public int value = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") OnCoinCollect?.Invoke(value);
        this.gameObject.SetActive(false);
    }
}
