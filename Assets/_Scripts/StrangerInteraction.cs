using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class StrangerInteraction : MonoBehaviour
{
    [Header("Audio and Subtitles")]
    [SerializeField] Dialog[] dialog;
    [SerializeField] Dialog[] InteruptDialog;
    [SerializeField] private TextMeshProUGUI subTitletext;

    private AudioSource AudioSource;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("DialogInteraction");
    }

    IEnumerator DialogInteraction()
    {
        ProgrammaticQuests.PhaseID currentPhase = GameManager.gameManager.phase;

        for (int i = 0; i < dialog.Length; i++)
        {
            AudioSource.clip = dialog[i].dialogAudio;
            subTitletext.text = dialog[i].dialogText;
            AudioSource.Play();

            yield return new WaitForSeconds(AudioSource.clip.length);

            if (currentPhase != GameManager.gameManager.phase)
            {
                AudioSource.clip = InteruptDialog[0].dialogAudio;
                subTitletext.text = InteruptDialog[0].dialogText;
                AudioSource.Play();
                break;
            }
        }
    }
}
