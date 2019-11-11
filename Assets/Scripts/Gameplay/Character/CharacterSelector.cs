using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterSelector : MonoBehaviour
{
    public Transform cam;
    public Vector3 offset;
    public CharacterControl characterControl;
    public Character[] characters;
    public float spacing;
    public float transitionSpeed;
    public float transparency = 0.5f;
    private GameObject[] charObjs;
    public CharacterDisplay charDisplay;
    private Renderer[] renderers;
    private Material[] materials;
    private float value;
    private int selection;
    private UnlockData data;

    void Awake()
    {
        InstantiateCharacters();
        HideUnselectedCharacters();
        data = SaveLoad.LoadUnlockData();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].unlocked = data.chars[i];
        }

        //Load character saved data

        //Assign recent character
    }


    // Update is called once per frame
    void Update()
    {
        selection = Selection();
    }
    private void InstantiateCharacters()
    {
        charObjs = new GameObject[characters.Length];
        materials = new Material[characters.Length];
        renderers = new Renderer[characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject obj = Instantiate(characters[i].characterModel);
            obj.transform.position = cam.up * (-1) * i * spacing + offset;
            renderers[i] = obj.GetComponentInChildren<Renderer>();
            materials[i] = renderers[i].material;
            charObjs[i] = obj;
        }
    }
    public void SetValue(float value)
    {
        this.value = value;
        for (int i = 0; i < characters.Length; i++)
        {
            charObjs[i].transform.position = cam.up * (-1) * (i - value) * spacing + offset;
            float alpha = 1 - Mathf.Clamp(Mathf.Abs(value - i) - transparency, 0, 1);
            if (i >= (selection - 1) && i <= (selection + 1)) materials[i].SetFloat("_Alpha", alpha);
        }
        charDisplay.SetAlpha(1 - Mathf.Clamp(Mathf.Abs(value - selection) * 2, 0, 1));
    }
    public void EnterCharacterSelect()
    {
        StartCoroutine(IShowUnselectedCharacters());
    }
    public void ExitCharacterSelect()
    {
        StartCoroutine(IHideUnselectedCharacters());
    }
    IEnumerator IHideUnselectedCharacters()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            if (i != selection) renderers[i].shadowCastingMode = ShadowCastingMode.Off;
            else renderers[i].shadowCastingMode = ShadowCastingMode.On;
        }
        float alpha = transparency;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * transitionSpeed;
            for (int i = 0; i < materials.Length; i++)
            {
                if (i >= (selection - 1) && i <= (selection + 1) && i != selection) materials[i].SetFloat("_Alpha", alpha);
            }
            yield return null;
        }
        for (int i = 0; i < materials.Length; i++)
        {
            if (i >= (selection - 1) && i <= (selection + 1) && i != selection) materials[i].SetFloat("_Alpha", 0);
        }
    }
    IEnumerator IShowUnselectedCharacters()
    {
        float alpha = 0;
        while (alpha < transparency)
        {
            alpha += Time.deltaTime * transitionSpeed;
            for (int i = 0; i < materials.Length; i++)
            {
                if (i >= (selection - 1) && i <= (selection + 1) && i != selection) materials[i].SetFloat("_Alpha", alpha);
            }
            yield return null;
        }
        for (int i = 0; i < materials.Length; i++)
        {
            if (i >= (selection - 1) && i <= (selection + 1) && i != selection) materials[i].SetFloat("_Alpha", 0.5f);
        }
    }
    public void HideUnselectedCharacters()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            if (i != selection) renderers[i].shadowCastingMode = ShadowCastingMode.Off;
        }
        selection = Selection();
        for (int i = 0; i < materials.Length; i++)
        {
            if (i != selection) materials[i].SetFloat("_Alpha", 0);
            else materials[i].SetFloat("_Alpha", 1);
        }
    }
    public int Selection()
    {
        return Mathf.RoundToInt(value);
    }
    public void ConfirmSelection()
    {

    }
    public void Unlock()
    {
        data.UnlockCharacter(selection);
        SaveLoad.SaveUnlockData(data);
    }
}
[System.Serializable]
public class Character
{
    public string characterName;
    public GameObject characterModel;
    public int price;
    [HideInInspector]
    public bool unlocked;
}