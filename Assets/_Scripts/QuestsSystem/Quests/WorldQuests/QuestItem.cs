using UnityEngine;
using ProgrammaticQuests;


namespace WorldQuests
{
    class QuestItem : MonoBehaviour
    {
        [SerializeField]
        QuestObjectName _quest_object_name;
        [SerializeField]
        internal bool deTaggable = true;

        internal QuestObjectName Quest_Object_Name { get => _quest_object_name;}
    }
}
