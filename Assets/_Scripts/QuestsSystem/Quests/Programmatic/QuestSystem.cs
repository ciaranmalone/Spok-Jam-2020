using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgrammaticQuests
{
    internal enum PhaseID
    {
        Phase0,
        Phase1,
        Phase2,
        Phase3,
        Phase4
    }

    internal enum QuestID
    {
        P0M0, //only for looping
        P1M1,
        P2M1,
        P2M2,
        P2M3,
        P2M4,
        P3M1,
        P3M2,
        P4M1,
        P4M2,
        OPT1,
        OPT2,
        OPT3,
    }

    enum QuestObjectName
    {
        PLAYER, TOILET_PAPER, DVD, MOP, BANANA, BLENDER, RUBBISH, MALK, SUSTOKEN
    }



    /// <summary>
    /// This class stores the info about:
    ///     -where the task sheets are
    ///     -events of quests
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        [SerializeField]
        internal Phase[] phases;
        [SerializeField]
        internal Quest[] bonusQuests;

        /// <summary>
        /// retrieve an existing quest by id, can be null if you suck at inpector drag and dropping :P
        /// </summary>
        /// <param name="quest_id">The id of the quest</param>
        /// <returns></returns>
        internal Quest getQuest(QuestID quest_id)
        {
            foreach (Phase phase in phases)
                foreach (Quest quest in phase.quests)
                    if (quest.quest_id == quest_id) return quest;
            foreach (Quest quest in bonusQuests)
                if (quest.quest_id == quest_id) return quest;
            return null;
        }
    }

    [System.Serializable]
    internal class Phase
    {
        [SerializeField]
        internal /*Dictionary<string,Quest>*/ Quest[] quests;
    }


    /// <summary>
    /// Wrapper for events that happen when a quest is being interacted with
    /// </summary>
    [System.Serializable]
    internal class Quest
    {
        [SerializeField]
        internal QuestID quest_id;
        [SerializeField]
        [Range(1,10)]
        internal int count = 1;
        [SerializeField]
        internal string description;
        [SerializeField]
        internal QuestObjectName objectName;
        [SerializeField]
        internal AudioClip clip;
    }
}