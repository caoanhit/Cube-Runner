using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public Text charName;
    public GameObject selectButton;
    public GameObject unlockButton;
    public Text price;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    public void SetData(Character character)
    {
        charName.text = character.characterName;
        price.text = character.price.ToString();
        if (character.unlocked)
        {
            selectButton.SetActive(true);
            unlockButton.SetActive(false);
        }
        else
        {
            selectButton.SetActive(false);
            unlockButton.SetActive(true);
        }
    }

}
