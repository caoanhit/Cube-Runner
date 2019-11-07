using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventListener))]
[CanEditMultipleObjects]
public class GameEventListenerEditor : Editor
{
    SerializedProperty Event;
    SerializedProperty Response;
    string eventName;

    void OnEnable()
    {
        Event = serializedObject.FindProperty("Event");
        eventName = Event.displayName;
        Response = serializedObject.FindProperty("Response");
    }
    public override void OnInspectorGUI()
    {
        EditorGUIUtility.labelWidth = 55.0f;
        EditorGUILayout.PropertyField(Event, new GUIContent("Event"));
        if (Event.isExpanded)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(Response, new GUIContent("Response"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
