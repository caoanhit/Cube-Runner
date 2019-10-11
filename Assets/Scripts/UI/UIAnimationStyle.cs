using UnityEngine;

[CreateAssetMenu]
public class UIAnimationStyle : ScriptableObject
{
    public AnimInfo openAnim;
    public AnimInfo closeAnim;

}
[System.Serializable]
public class AnimInfo
{
    public AnimationClip anim;
    public float speed;
}