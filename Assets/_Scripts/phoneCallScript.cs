using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phoneCallScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] AudioClips;
    [SerializeField] private string[] subtitles;
    [SerializeField] private TextMeshProUGUI subTitleText;
    private AudioSource AudioSource;

    public string phase = "phoneCall1";
    public bool phoneAnswered = false;
    public bool Ringing = false;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        subTitleText.text = "";
        GameEvents.current.onPhaseChange += PhasePhoneCall;
    }

    // Update is called once per frame
    void PhasePhoneCall(string phase)
    {
        if(this.phase == phase) {
            StartCoroutine(StartRinging());
        }
    }
    IEnumerator dialBegin() {
        GameEvents.current.spawnNextNote();
        for(int i = 1; i < AudioClips.Length; i++)
        {
            AudioSource.clip = AudioClips[i];
            subTitleText.text = subtitles[i];
            AudioSource.Play();
            yield return new WaitForSeconds(AudioSource.clip.length);   
        }
        subTitleText.text = "";
        Destroy(this);
    }

    IEnumerator StartRinging() {
        Ringing = true; 

        AudioSource.clip = AudioClips[0];
        subTitleText.text = subtitles[0];
        AudioSource.loop = true;
        AudioSource.Play();

        while(!phoneAnswered)
        {
            yield return null;
        }

        phoneAnswered = false;
        AudioSource.loop = false;
        Ringing = false; 

        StartCoroutine(dialBegin());
    }
}
