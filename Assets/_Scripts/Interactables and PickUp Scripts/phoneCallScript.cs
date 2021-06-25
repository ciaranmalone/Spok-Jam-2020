using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class phoneCallScript : MonoBehaviour
{
    [SerializeField] private Dialog[] dialog;
    [SerializeField] private Dialog ringNoise;
    [SerializeField] private Dialog HangUp;

    [SerializeField] private TextMeshProUGUI subTitleText;
    private AudioSource AudioSource;

    public string phase = "phoneCall1";
    public bool phoneAnswered = false;
    public bool Ringing = false;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        subTitleText.text = "";
    }

    internal void PhasePhoneCall(string phase)
    {
        if (this.phase == phase)
        {
            StartCoroutine(StartRinging());
        }
    }

    IEnumerator DialogInteraction() {
        GameManager.gameManager.spawnNextTaskSheet();
        for(int i = 1; i < dialog.Length; i++)
        {
            AudioSource.clip = dialog[i].dialogAudio;
            subTitleText.text = dialog[i].dialogText;
            AudioSource.Play();
            yield return new WaitForSeconds(AudioSource.clip.length);   
        }
        subTitleText.text = "";
        Destroy(this);
    }

    IEnumerator StartRinging() {
        Ringing = true; 

        AudioSource.clip = ringNoise.dialogAudio;
        subTitleText.text = ringNoise.dialogText;
        AudioSource.loop = true;
        AudioSource.Play();

        while(!phoneAnswered)
        {
            yield return null;
        }

        phoneAnswered = false;
        AudioSource.loop = false;
        Ringing = false; 

        StartCoroutine(DialogInteraction());
    }
}