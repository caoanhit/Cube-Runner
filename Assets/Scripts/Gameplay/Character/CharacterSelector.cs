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
    public Material normal;
    public Material select;
    private GameObject[] charObjs;
    public CharacterDisplay charDisplay;
    public ScrollSnap scroll;

    private Renderer[] renderers;
    private float value;
    private int selection;
    private UnlockData data;
    private References references;
    private int lastSelection;
    void Awake()
    {
        InstantiateCharacters();
        data = SaveLoad.LoadUnlockData();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].unlocked = data.chars[i];
        }
        //Load character saved data
        references = SaveLoad.LoadReferences();
        this.value = references.character;
        selection = references.character;
        lastSelection = selection;
        for (int i = 0; i < characters.Length; i++)
        {
            charObjs[i].transform.position = cam.up * (-1) * (i - value) * spacing + offset;
        }
        HideUnselectedCharacters();
        ConfirmSelection();
        SetDisplay();
    }


    // Update is called once per frame
    void Update()
    {
        selection = Selection();
        if (selection != lastSelection)
        {
            SetDisplay();
        }
        lastSelection = selection;
    }
    private void InstantiateCharacters()
    {
        charObjs = new GameObject[characters.Length];
        renderers = new Renderer[characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject obj = Instantiate(characters[i].characterModel);
            obj.transform.position = cam.up * (-1) * i * spacing + offset;
            renderers[i] = obj.GetComponentInChildren<Renderer>();
            charObjs[i] = obj;
        }
    }
    public void SetValue(float value)
    {
        this.value = value;
        for (int i = 0; i < characters.Length; i++)
        {
            if (i >= (selection - 1) && i <= (selection + 1))
            {
                charObjs[i].transform.position = cam.up * (-1) * (i - value) * spacing + offset;
                float alpha = 1 - Mathf.Clamp(Mathf.Abs(value - i) - transparency, 0, 1);
                charObjs[i].transform.localScale = Vector3.one * (1 - Mathf.Clamp(Mathf.Abs(value - i) - 0.5f, 0, 1));
                renderers[i].material.SetFloat("_Alpha", alpha);
            }
        }
        charDisplay.SetAlpha(Mathf.Clamp(1 - Mathf.Abs(value - selection) * 3, 0, 1));
    }
    public void SetDisplay()
    {
        charDisplay.SetData(characters[selection]);
    }
    public void EnterCharacterSelect()
    {
        scroll.SetValue(references.character);
        data = SaveLoad.LoadUnlockData();
        charObjs[selection].gameObject.transform.parent = null;
        StartCoroutine(IShowUnselectedCharacters());
    }
    public void ExitCharacterSelect()
    {
        ConfirmSelection();
        StartCoroutine(IHideUnselectedCharacters());
    }
    IEnumerator IHideUnselectedCharacters()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (i != selection) renderers[i].shadowCastingMode = ShadowCastingMode.Off;
            else renderers[i].shadowCastingMode = ShadowCastingMode.On;
        }
        float alpha = transparency;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * transitionSpeed;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (i >= (selection - 1) && i <= (selection + 1) && i != selection) renderers[i].material.SetFloat("_Alpha", alpha);
            }
            yield return null;
        }
        for (int i = 0; i < renderers.Length; i++)
        {
            if (i >= (selection - 1) && i <= (selection + 1))
            {
                renderers[i].material = normal;
                if (i != selection) renderers[i].material.SetFloat("_Alpha", 0);
            }
        }
    }
    IEnumerator IShowUnselectedCharacters()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = select;
            if (i != selection) renderers[i].material.SetFloat("_Alpha", 0);
            else renderers[i].material.SetFloat("_Alpha", 1);
        }
        float alpha = 0;
        while (alpha < transparency)
        {
            alpha += Time.deltaTime * transitionSpeed;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (i >= (selection - 1) && i <= (selection + 1) && i != selection)
                {
                    renderers[i].material.SetFloat("_Alpha", alpha);
                }
            }
            yield return null;
        }
        for (int i = 0; i < renderers.Length; i++)
        {
            if (i >= (selection - 1) && i <= (selection + 1) && i != selection)
            {
                renderers[i].material.SetFloat("_Alpha", transparency);
            }
        }
    }
    IEnumerator IHideCharacter()
    {
        renderers[selection].shadowCastingMode = ShadowCastingMode.Off;
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * transitionSpeed;
            renderers[selection].material.SetFloat("_Alpha", alpha);
            yield return null;
        }
        renderers[selection].material.SetFloat("_Alpha", 0);
    }
    IEnumerator IShowCharacter()
    {

        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * transitionSpeed;
            renderers[selection].material.SetFloat("_Alpha", alpha);
            yield return null;
        }
        renderers[selection].material.SetFloat("_Alpha", 1);
        renderers[selection].shadowCastingMode = ShadowCastingMode.On;
    }
    public void HideCharacter()
    {
        StartCoroutine(IHideCharacter());
    }
    public void ShowCharacter()
    {
        StartCoroutine(IShowCharacter());
    }
    public void HideUnselectedCharacters()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = normal;
            if (i != selection)
            {
                renderers[i].shadowCastingMode = ShadowCastingMode.Off;
                renderers[i].material.SetFloat("_Alpha", 0);
                charObjs[i].transform.localScale = Vector3.one * 0.5f;
            }
            else
            {
                renderers[i].material.SetFloat("_Alpha", 1);
            }
        }
    }
    public int Selection()
    {
        return Mathf.Clamp(Mathf.RoundToInt(value), 0, characters.Length);
    }
    public void ConfirmSelection()
    {
        charObjs[selection].gameObject.transform.parent = characterControl.gameObject.transform;
        references = SaveLoad.LoadReferences();
        references.character = selection;
        SaveLoad.SaveReferences(references);
    }
    public void Unlock()
    {
        if (!data.chars[selection] && ScoreManager.Instance.RemoveCoin(characters[selection].price))
        {
            data.UnlockCharacter(selection);
            characters[selection].unlocked = true;
            charDisplay.SetData(characters[selection]);
            SaveLoad.SaveUnlockData(data);

        }
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