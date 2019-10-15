using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldElement : MonoBehaviour
{
    public WorldType type;
    Material material;
    int id;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        id = Shader.PropertyToID("_Alpha");
        WorldManager.instance.Assign(type, this.gameObject);
        if (WorldManager.instance.currentType != type) this.gameObject.SetActive(false);
    }
    public void Transition(WorldType type, float alpha)
    {
        if (type == this.type)
        {
            material.SetFloat(id, alpha);
        }
    }
    private void OnEnable()
    {
        WorldManager.instance.OnWorldTransition += Transition;
    }
    private void OnDisable()
    {
        WorldManager.instance.OnWorldTransition -= Transition;
    }
}
