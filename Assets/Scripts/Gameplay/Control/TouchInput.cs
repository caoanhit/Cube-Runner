using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    public static TouchInput Instance
    {
        get { return s_Instance; }
    }
    public float tapDistance;
    public float holdDelay;
    public float swipeDistance;
    public float swipeDelay;
    protected static TouchInput s_Instance;
    #region Control
    public bool swipeUp { get; private set; }
    public bool swipeDown { get; private set; }
    public bool swipeRight { get; private set; }
    public bool swipeLeft { get; private set; }
    public bool tap { get; private set; }
    public bool hold { get; private set; }
    #endregion
    Vector3 startPoint;
    float holdTime;
    void Awake()
    {

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            Destroy(this.gameObject);
    }

    private void Update()
    {
        ResetControl();
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdTime = 0;
                startPoint = Input.mousePosition;
            }
            if (holdTime > holdDelay && !EventSystem.current.IsPointerOverGameObject()) hold = true;
            holdTime += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 dir = Input.mousePosition - startPoint;
            if (dir.magnitude < tapDistance && holdTime <= holdDelay && !EventSystem.current.IsPointerOverGameObject())
            {
                tap = true;
                //Debug.Log("Tap");
            }
            if (dir.magnitude > swipeDistance && holdTime < swipeDelay)
            {
                Vector3 d = dir.normalized;
                if (Mathf.Abs(d.x) >= Mathf.Abs(d.y) && d.x > 0)
                {
                    swipeRight = true;
                    //Debug.Log("Swipe Right");
                }
                if (Mathf.Abs(d.x) >= Mathf.Abs(d.y) && d.x < 0)
                {
                    swipeLeft = true;
                    //Debug.Log("Swipe Left");
                }
                if (Mathf.Abs(d.x) < Mathf.Abs(d.y) && d.y > 0)
                {
                    swipeUp = true;
                    //Debug.Log("Swipe Up");
                }
                if (Mathf.Abs(d.x) < Mathf.Abs(d.y) && d.y < 0)
                {
                    swipeDown = true;
                    //Debug.Log("Swipe Down");
                }
            }
            holdTime = 0;
        }

#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                holdTime = 0;
                startPoint = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (holdTime > holdDelay && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)) hold = true;
                holdTime += Time.deltaTime;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 dir = touch.position - new Vector2(startPoint.x, startPoint.y);
                if (dir.magnitude < tapDistance && holdTime <= holdDelay && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    tap = true;
                    //Debug.Log("Tap");
                }
                if (dir.magnitude > swipeDistance && holdTime < swipeDelay)
                {
                    Vector3 d = dir.normalized;
                    if (Mathf.Abs(d.x) >= Mathf.Abs(d.y) && d.x > 0)
                    {
                        swipeRight = true;
                        //Debug.Log("Swipe Right");
                    }
                    if (Mathf.Abs(d.x) >= Mathf.Abs(d.y) && d.x < 0)
                    {
                        swipeLeft = true;
                        //Debug.Log("Swipe Left");
                    }
                    if (Mathf.Abs(d.x) < Mathf.Abs(d.y) && d.y > 0)
                    {
                        swipeUp = true;
                        //Debug.Log("Swipe Up");
                    }
                    if (Mathf.Abs(d.x) < Mathf.Abs(d.y) && d.y < 0)
                    {
                        swipeDown = true;
                        //Debug.Log("Swipe Down");
                    }
                }
                holdTime = 0;
            }

        }
    }
    private void ResetControl()
    {
        swipeUp = false;
        swipeDown = false;
        swipeLeft = false;
        swipeRight = false;
        tap = false;
        hold = false;
    }

}
