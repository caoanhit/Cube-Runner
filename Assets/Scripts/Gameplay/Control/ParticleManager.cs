using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
    }
    public ParticleData[] particles;

    Dictionary<string, ParticleSystem> particleDict;
    void Start()
    {
        particleDict = new Dictionary<string, ParticleSystem>();
        foreach (ParticleData data in particles)
        {
            particleDict.Add(data.name, data.particle);
        }
    }
    public void Play(string name, Vector3 position)
    {
        particleDict[name].gameObject.transform.position = position;
        particleDict[name].Play();
    }

}
[System.Serializable]
public class ParticleData
{
    public string name;
    public ParticleSystem particle;
}