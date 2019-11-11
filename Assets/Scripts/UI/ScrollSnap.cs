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
    public IntEvent OnSelectedChange;
    public FloatEvent OnValueChange;
    public Button previousButton;
    public Button nextButton;
    private ScrollRect scrollRect;
    private List<RectTransform> items;
    private bool dragging;
    private float releaseSpeed, planeDistance;
    private Vector2 previousPosition;
    private float value;
    private float lastValue;
    private int selectedValue;
    private int lastSelectedValue;
    private bool forceMove;
    private int forceValue;
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
        previousButton?.onClick.AddListener(Previous);
        nextButton?.onClick.AddListener(Next);
        CheckButtons();
    }
    private void Update()
    {
        selectedValue = GetSelectedValue();
        if (selectedValue != lastSelectedValue)
        {
            OnSelectedChange.Invoke(selectedValue);
            CheckButtons();
        }
        lastSelectedValue = selectedValue;
        Vector2 snapPos = Vector2.up * selectedValue * itemHeight;
        if (!dragging && scrollRect.velocity.magnitude < minVelocity)
        {
            if (forceMove)
            {
                scrollRect.content.anchoredPosition = Vector2.MoveTowards(scrollRect.content.anchoredPosition, Vector2.up * forceValue * itemHeight, snapSpeed * 2 * Time.deltaTime);
                if (selectedValue == forceValue) forceMove = false;
            }
            else scrollRect.content.anchoredPosition = Vector2.MoveTowards(scrollRect.content.anchoredPosition, snapPos, snapSpeed * Time.deltaTime);
        }
        value = GetValue();
        if (value != lastValue)
            OnValueChange.Invoke(value);
        lastValue = value;
    }
    public void CheckButtons()
    {
        if (selectedValue == 0)
        {
            previousButton?.gameObject.SetActive(false);
        }
        else previousButton?.gameObject.SetActive(true);
        if (selectedValue == items.Count - 2)
        {
            nextButton?.gameObject.SetActive(false);
        }
        else nextButton?.gameObject.SetActive(true);
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
    public void SetSelectedValue(int value)
    {
        selectedValue = value;
    }
    public void SetDirectValue(int value)
    {
        selectedValue = value;
        lastSelectedValue = value;
        this.value = value;
        scrollRect.content.anchoredPosition = Vector2.up * selectedValue * itemHeight;
    }
    public float GetValue()
    {
        return scrollRect.content.anchoredPosition.y / itemHeight;
    }
    public int GetSelectedValue()
    {
        return Mathf.Clamp(Mathf.RoundToInt(scrollRect.content.anchoredPosition.y / itemHeight), 0, items.Count - 1);
    }
    [ContextMenu("Next")]
    public void Next()
    {
        if (selectedValue < items.Count - 1 && forceValue < items.Count - 1)
        {
            forceMove = true;
            forceValue = selectedValue + 1;
        }
    }
    [ContextMenu("Previous")]
    public void Previous()
    {
        if (selectedValue > 0 && forceValue > 0)
        {
            forceMove = true;
            forceValue = selectedValue - 1;
        }
    }
}
[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
