using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkingScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] AudioClips;
    AudioSource AudioSource;
    [SerializeField] private string[] subtitles;
    [SerializeField] private TextMeshProUGUI mtext;

    private bool playedAudio = false;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        mtext.text = "";
    }

    private void OnTriggerEnter(Collider other) 
   {
       if(!playedAudio){
            StartCoroutine(dialBegin());
            playedAudio = true;

            LeanTween.moveSpline(gameObject, new Vector3[]{new Vector3(0f,0f,0f),new Vector3(1f,0f,0f),new Vector3(1f,0f,0f),new Vector3(1f,0f,1f)}, 1.5f).setEase(LeanTweenType.easeOutQuad).setOrientToPath(true);
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
        Destroy(gameObject);     
    }
}
