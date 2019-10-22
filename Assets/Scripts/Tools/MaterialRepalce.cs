using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialRepalce : MonoBehaviour
{
    public Material material;
    [ContextMenu("Replace")]
    public void Replace()
    {
        Object[] images = Resources.FindObjectsOfTypeAll(typeof(Image));
        foreach (Object obj in images)
        {
            Image gameObj = (Image)obj;
            gameObj.material = null;
        }
    }
}
