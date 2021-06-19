using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WorldQuests.EggQuest))]
public class EggQuestEditor : Editor
{

    SerializedProperty canvas;
    SerializedProperty itemToCollideWith;
    SerializedProperty quest_id;

    private void OnEnable()
    {
        itemToCollideWith = serializedObject.FindProperty("itemToCollideWith");
        canvas = serializedObject.FindProperty("canvas");
        quest_id = serializedObject.FindProperty("quest_id");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((WorldQuests.EggQuest)target), typeof(WorldQuests.EggQuest), false);

        EditorGUILayout.PropertyField(canvas, new GUIContent("Canvas Quest?"));

        if (canvas.boolValue)
        {
            EditorGUILayout.PropertyField(quest_id, new GUIContent("Quest ID"));
        }
        else
        {
            EditorGUILayout.PropertyField(itemToCollideWith, new GUIContent("Item To Collide With"));
        }

        serializedObject.ApplyModifiedProperties();
    }

}
