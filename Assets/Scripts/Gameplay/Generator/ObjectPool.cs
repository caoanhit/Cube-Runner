using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public Pool[] pools;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);

        objectpool = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.amount; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.prefab);
                obj.transform.SetParent(this.transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            objectpool.Add(pool.name, queue);
        }
    }
    private Dictionary<string, Queue<GameObject>> objectpool;
    public GameObject Spawn(string name, Vector3 postion, Quaternion rotation)
    {
        if (objectpool.ContainsKey(name))
        {
            GameObject obj = objectpool[name].Dequeue();
            obj.SetActive(true);
            obj.transform.position = postion;
            obj.transform.rotation = rotation;
            objectpool[name].Enqueue(obj);
            return obj;
        }
        return null;
    }
}
[System.Serializable]
public class Pool
{
    public string name;
    public int amount;
    public GameObject prefab;
}
