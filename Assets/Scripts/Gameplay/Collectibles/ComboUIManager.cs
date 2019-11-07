using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUIManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform cam;
    public IntVariable combo;
    public IntVariable additionalScore;

    private Queue<ComboUI> comboUIs;
    private void Start()
    {
        comboUIs = new Queue<ComboUI>();
        ComboUI[] uis = GetComponentsInChildren<ComboUI>();
        foreach (ComboUI ui in uis)
        {
            ui.gameObject.SetActive(false);
            comboUIs.Enqueue(ui);
        }
    }
    private void OnEnable()
    {
        Checker.OnPerfectPos += OnPerfect;
    }
    private void OnDisable()
    {
        Checker.OnPerfectPos -= OnPerfect;
    }
    public void OnPerfect(Vector3 pos)
    {
        ComboUI ui = comboUIs.Dequeue();
        Vector3 p = pos + cam.right * offset.x + cam.up * offset.y + cam.forward * offset.z;
        ui.Show(p, Quaternion.LookRotation(cam.forward), combo);
        additionalScore.ApplyChange(combo);
        comboUIs.Enqueue(ui);
    }
}
