using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playDVD : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private string objectiveItem ="fitnessDVD";

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ObjectiveItem>() != null && other.GetComponent<ObjectiveItem>().ItemName == objectiveItem)
        {
            videoPlayer.Play();
        }
    }
}
