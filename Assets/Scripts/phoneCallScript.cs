using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phoneCallScript : MonoBehaviour
{
    [SerializeField] private string phase = "phone call";
    [SerializeField] private AudioClip[] AudioClips;
    [SerializeField] private string[] subtitles;
    [SerializeField] private TextMeshProUGUI mtext;
    AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        mtext.text = "";
        GameEvents.current.onPhaseChange += PhasePhoneCall;
    }

    // Update is called once per frame
    void PhasePhoneCall(string phase)
    {
        if(this.phase == phase) {
            StartCoroutine(dialBegin());
        }
    }
    IEnumerator dialBegin() {
        for(int i = 0; i < AudioClips.Length; i++)
        {
            AudioSource.clip = AudioClips[i];
            mtext.text = subtitles[i];
            AudioSource.Play();
            yield return new WaitForSeconds(AudioSource.clip.length);   
        }
        mtext.text = "";
    }
}
