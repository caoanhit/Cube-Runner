using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageGenerator : MonoBehaviour
{
    public int onScreenAmount;
    public FoliageSet[] foliageSets;
    private GameObject[][] prefabs;
    private Queue<int>[] previousIds;
    public void Awake()
    {
        previousIds = new Queue<int>[foliageSets.Length];
        prefabs = new GameObject[foliageSets.Length][];
        for (int i = 0; i < foliageSets.Length; i++)
        {
            previousIds[i] = new Queue<int>();
            prefabs[i] = new GameObject[foliageSets[i].prefabs.Length];
        }
    }
    public void SpawnFoliage(int type, Vector3 position)
    {
        int id = Random.Range(0, foliageSets[type].prefabs.Length);
        while (previousIds[type].Contains(id))
        {
            id = Random.Range(0, foliageSets[type].prefabs.Length);
        }
        if (previousIds[type].Count >= onScreenAmount)
        {
            int obj = previousIds[type].Dequeue();
            prefabs[type][obj].SetActive(false);
        }
        previousIds[type].Enqueue(id);
        if (prefabs[type][id] == null)
        {
            GameObject obj = Instantiate(foliageSets[type].prefabs[id]);
            obj.transform.position = position;
            prefabs[type][id] = obj;
        }
        else
        {
            prefabs[type][id].transform.position = position;
            prefabs[type][id].SetActive(true);
        }
    }
}
[System.Serializable]
public class FoliageSet
{
    public GameObject[] prefabs;
}