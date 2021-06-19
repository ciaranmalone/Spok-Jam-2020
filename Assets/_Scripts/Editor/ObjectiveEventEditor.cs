using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FakeToFromAnimation))]
public class ObjectiveEventEditor : Editor
{
    private SerializedProperty fromRequired;
    private SerializedProperty playSound;
    private SerializedProperty fromObject;
    private SerializedProperty toObject;
    private SerializedProperty clip;

    private void OnEnable()
    {
        fromRequired = serializedObject.FindProperty("fromRequired");
        playSound = serializedObject.FindProperty("playSound");
        fromObject = serializedObject.FindProperty("fromObject");
        toObject = serializedObject.FindProperty("toObject");
        clip = serializedObject.FindProperty("clip");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FakeToFromAnimation)target), typeof(FakeToFromAnimation), false);
        //EditorGUILayout.PropertyField(, new GUIContent("Script"));
        drawHeader("Requirements");
        EditorGUILayout.PropertyField(fromRequired, new GUIContent("Is From Required?"));
        EditorGUILayout.PropertyField(playSound, new GUIContent("Will it play sound?"));

        drawHeader("Objects");
        if (fromRequired.boolValue)
        {
            EditorGUILayout.PropertyField(fromObject, new GUIContent("From Object"));
        }
        EditorGUILayout.PropertyField(toObject, new GUIContent("To Object"));


        if (playSound.boolValue)
        {
            drawHeader("Audio");
            EditorGUILayout.PropertyField(clip, new GUIContent("Clip"));
        }


        serializedObject.ApplyModifiedProperties();
    }

    private void drawHeader(string text)
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
    }
}
