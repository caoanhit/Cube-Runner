using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(Animator))]
public class ButtonAnimation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.SetBool("isPressed", true);
    }
    public void OnPointerUpDelegate(PointerEventData data)
    {
        anim.SetBool("isPressed", false);
    }
    public void OnPointerEnterDelegate(PointerEventData data)
    {
        anim.SetBool("isHover", true);
    }
    public void OnPointerExitDelegate(PointerEventData data)
    {
        anim.SetBool("isHover", true);
    }
}
