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
    [SerializeField] Dialog LeavingDialog;
    [SerializeField] private TextMeshProUGUI subTitletext;
    [SerializeField]internal ProgrammaticQuests.QuestID quest_id;

    private AudioSource AudioSource;
    private Renderer renderer;
    private bool conversationStarted = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !conversationStarted)
        {
            conversationStarted = true;
            StartCoroutine("DialogInteraction");
        }
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
        StartCoroutine("Leave");
    }

    IEnumerator Leave()
    {
        AudioSource.clip = LeavingDialog.dialogAudio;
        subTitletext.text = LeavingDialog.dialogText;
        AudioSource.Play();

        float opacityMaterial = 1f;
        while (opacityMaterial > 0)
        {
            renderer.material.color = new Color(0,0,0, opacityMaterial);
            yield return new WaitForSeconds(.05f);
            opacityMaterial -= .1f;
        }

        yield return new WaitForSeconds(AudioSource.clip.length);
        subTitletext.text = "";
        GameManager.gameManager.CreateBonusQuest(quest_id);

        Destroy(gameObject);
    }
}
