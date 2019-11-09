using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    public float spacing;
    private float value;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetValue(float value)
    {
        this.value = value;
    }
    public int Selection()
    {
        return Mathf.RoundToInt(value);
    }
}
[System.Serializable]
public class Character
{

}