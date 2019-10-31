using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SequencedUI : Animated
{
    public bool ovelap;
    public float spacing = 0.2f;
    List<Animated> items;
    private void Start()
    {
        items = new List<Animated>();
        foreach (Transform t in transform)
        {
            Animated a = t.GetComponent<Animated>();
            if (a != null)
            {
                items.Add(a);
            }
        }
    }
    [ContextMenu("Hide")]
    public override Sequence Hide()
    {

        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < items.Count; i++)
        {
            if (ovelap)
                sequence.Insert(spacing * i, items[i].Hide());
            else
                sequence.Append(items[i].Hide());
        }
        sequence.AppendCallback(() => gameObject.SetActive(false));
        return sequence;
    }
    [ContextMenu("Show")]
    public override Sequence Show()
    {
        gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < items.Count; i++)
        {
            if (ovelap)
                sequence.Insert(spacing * i, items[i].Show());
            else
                sequence.Append(items[i].Show());
        }
        return sequence;
    }
}
