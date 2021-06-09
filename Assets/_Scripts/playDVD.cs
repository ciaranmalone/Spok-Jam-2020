using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playDVD : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private ProgrammaticQuests.QuestObjectName objectiveItem = ProgrammaticQuests.QuestObjectName.TOILET_PAPER;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WorldQuests.QuestItem>() != null && other.GetComponent<WorldQuests.QuestItem>().Quest_Object_Name == objectiveItem)
        {
            videoPlayer.Play();
        }
    }
}
