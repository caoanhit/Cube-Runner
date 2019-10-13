using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollViewSnapper : MonoBehaviour
{
    ScrollRect scrollRect;
    public float itemWidth;
    public float lerpSpeed;
    public float snapSpeed;
    Vector2 origin;
    int currentItem;
    bool isDraging;
    RectTransform cont;
    int value;
    Vector2 dest;
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        cont = scrollRect.content;
        origin = cont.anchoredPosition;
    }
    void Update()
    {
        if (!isDraging)
        {
            value = Value();
            MoveToValue();
        }
    }
    int Value()
    {
        return Mathf.RoundToInt((cont.anchoredPosition.x - origin.x) / itemWidth);
    }
    void MoveToValue()
    {
        dest = new Vector2(value * itemWidth + origin.x, cont.anchoredPosition.y);
        float dist = Vector2.Distance(cont.anchoredPosition, dest);
        if (dist > 5)
            scrollRect.velocity = Vector2.Lerp(scrollRect.velocity, (dest - cont.anchoredPosition) * snapSpeed, lerpSpeed * Time.deltaTime);
        else
        {
            scrollRect.velocity = Vector2.zero;
            cont.anchoredPosition = dest;
        }
    }
    public void StartDrag()
    {
        isDraging = true;
    }
    public void StopDrag()
    {
        isDraging = false;
    }

}
