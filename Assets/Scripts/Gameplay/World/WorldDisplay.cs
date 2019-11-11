using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldDisplay : MonoBehaviour
{
    public Text worldName;
    public Text price;
    public GameObject selectButton;
    public GameObject unlockButton;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    public void SetData(string name, int price, bool unlocked)
    {
        worldName.text = name;
        this.price.text = price.ToString();
        if (unlocked)
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
