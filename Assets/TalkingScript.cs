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
            StartCoroutine(enterStore());
            playedAudio = true;

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
        StartCoroutine(leaveStore());    
    }

    IEnumerator enterStore() {
        yield return new WaitForSeconds(1f); 
        LeanTween.moveLocal(gameObject, new Vector3(-58f, 2f, -26f), 5f).setEaseInSine();
        yield return new WaitForSeconds(5f);   
        LeanTween.moveLocal(gameObject, new Vector3(-58f, 2f, -15f), 5f).setEaseOutCirc();

    }
    IEnumerator leaveStore() {
        LeanTween.moveLocal(gameObject, new Vector3(-58f, 2f, -139f), 10f).setEaseInCirc();
        yield return new WaitForSeconds(10f);   
        Destroy(gameObject);    
    }
}
