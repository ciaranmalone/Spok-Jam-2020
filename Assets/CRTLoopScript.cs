using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CRTLoopScript : MonoBehaviour
{
    Material CRTMaterial;
    [SerializeField] VideoClip staticVid;
    [SerializeField] internal SusToken.Token token;
    VideoPlayer videoPlayer;

    void Start()
    {
        CheckTokensCCTV();
    }

    internal void CheckTokensCCTV()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.enabled = false;

        if (GameManager.gameManager.loopCount >= 1)
        {
            if (GameManager.gameManager.susTokens[(int)token])
                videoPlayer.clip = staticVid;

            videoPlayer.enabled = true;
        }
    }
}