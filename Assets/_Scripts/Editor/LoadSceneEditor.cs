using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadScene))]
public class LoadSceneEditor : Editor
{
    SerializedProperty sceneName;
    SerializedProperty areaToTeleport;
    SerializedProperty setDestination;

    private void OnEnable()
    {
        sceneName = serializedObject.FindProperty("sceneName");
        areaToTeleport = serializedObject.FindProperty("areaToTeleport");
        setDestination = serializedObject.FindProperty("setDestination");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((LoadScene)target), typeof(LoadScene), false);

        EditorGUILayout.PropertyField(sceneName, new GUIContent("Scene Name"));

        EditorGUILayout.PropertyField(setDestination, new GUIContent("Set Destination"));

        if (setDestination.boolValue)
        {
            EditorGUILayout.PropertyField(areaToTeleport, new GUIContent("Area To Teleport"));
        } 
        else
        {
            EditorGUILayout.LabelField(">:(");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
