using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class AnimatedButton : AnimatedUI
{
    // Start is called before the first frame update
    public UIAnimation highlightAnim;
    public UIAnimation pressedAnim;
    public UIAnimation onClickAnim;
    protected override void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClick());
        base.Start();
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        AddEvent(eventTrigger, EventTriggerType.PointerDown, (data) => { OnPointerDownDelegate((PointerEventData)data); });
        AddEvent(eventTrigger, EventTriggerType.PointerUp, (data) => { OnPointerUpDelegate((PointerEventData)data); });
        AddEvent(eventTrigger, EventTriggerType.PointerEnter, (data) => { OnPointerEnterDelegate((PointerEventData)data); });
        AddEvent(eventTrigger, EventTriggerType.PointerExit, (data) => { OnPointerExitDelegate((PointerEventData)data); });
    }
    private void AddEvent(EventTrigger trigger, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        Press();
    }
    public void OnPointerUpDelegate(PointerEventData data)
    {
        Unpress();
    }
    public void OnPointerEnterDelegate(PointerEventData data)
    {
        Highlight();
    }
    public void OnPointerExitDelegate(PointerEventData data)
    {
        Unhightlight();
    }
    public Sequence Highlight()
    {
        return highlightAnim.Show(data);
    }
    public Sequence Unhightlight()
    {
        return highlightAnim.Hide(data);
    }
    public Sequence Press()
    {
        return pressedAnim.Show(data);
    }
    public Sequence Unpress()
    {
        return pressedAnim.Hide(data);
    }
    public Sequence OnClick()
    {
        return onClickAnim.Show(data);
    }
}
