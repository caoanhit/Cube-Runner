using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float itemHeight = 240;
    public float minVelocity;
    public float snapSpeed;
    public IntEvent OnValueChange;
    public FloatEvent OnPosChange;
    private ScrollRect scrollRect;
    private List<RectTransform> items;
    private bool dragging;
    private float releaseSpeed, planeDistance;
    private Vector2 previousPosition;
    private float pos;
    private int selectedValue;
    private int lastValue;
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        items = scrollRect.content.GetComponentsInChildren<RectTransform>().ToList();
        items.RemoveAt(0);
    }
    private void Update()
    {
        selectedValue = GetValue();
        if (selectedValue != lastValue) OnValueChange.Invoke(selectedValue);
        lastValue = selectedValue;
        Vector2 snapPos = Vector2.up * selectedValue * itemHeight;
        if (!dragging && scrollRect.velocity.magnitude < minVelocity)
        {
            scrollRect.content.anchoredPosition = Vector2.MoveTowards(scrollRect.content.anchoredPosition, snapPos, snapSpeed * Time.deltaTime);
        }
        pos = GetPos();
        OnPosChange.Invoke(pos);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.inertia = true;
        dragging = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        releaseSpeed = scrollRect.velocity.magnitude;
        dragging = false;
    }
    public void SetValue(int value)
    {
        selectedValue = value;
    }
    public void SetDirectValue(int value)
    {
        selectedValue = value;
        lastValue = value;
        pos = value;
        scrollRect.content.anchoredPosition = Vector2.up * selectedValue * itemHeight;
    }
    public float GetPos()
    {
        return scrollRect.content.anchoredPosition.y / itemHeight;
    }
    public int GetValue()
    {
        return Mathf.Clamp(Mathf.RoundToInt(scrollRect.content.anchoredPosition.y / itemHeight), 0, items.Count - 1);
    }
}
[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
