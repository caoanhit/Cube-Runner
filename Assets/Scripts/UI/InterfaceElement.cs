using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CanvasGroup))]
public class InterfaceElement : MonoBehaviour
{
    public UIAnimationStyle style;

    protected Animation anim;

#if UNITY_EDITOR
    private void Reset()
    {
        Animation a = GetComponent<Animation>();
        string[] animFiles = Directory.GetFiles(Application.dataPath+"/Animation/UI", "*.anim", SearchOption.AllDirectories);
        foreach(string file in animFiles)
        {
            string assetPath = "Assets" + file.Replace(Application.dataPath, "").Replace('\\', '/');
            AnimationClip clip = (AnimationClip)AssetDatabase.LoadAssetAtPath(assetPath,typeof(AnimationClip));
            a.AddClip(clip, clip.name);
        }
    }
#endif
}
