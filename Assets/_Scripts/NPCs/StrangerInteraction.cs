using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using ProgrammaticQuests;
using WorldQuests;

public class StrangerInteraction : MonoBehaviour
{
    [Header("Audio and Subtitles")]
    [SerializeField] Dialog[] dialog;
    [SerializeField] Dialog noItemDialog;
    [SerializeField] Dialog LeavingDialog;
    [SerializeField] private TextMeshProUGUI subTitletext;
    [SerializeField] internal ProgrammaticQuests.QuestID quest_id;
    [SerializeField] private QuestObjectName keyObjectName;
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
        QuestItem otherItem = other.GetComponent<QuestItem>();
        Debug.Log($"HERE: {otherItem}");
        if (otherItem != null)
        {
            if (otherItem.Quest_Object_Name == keyObjectName && !conversationStarted)
            {
                conversationStarted = true;
                StartCoroutine("DialogInteraction", other.GetComponent<SusToken.SusToken>().token);
            }
            else if (!conversationStarted)
            {
                StartCoroutine("DialogInteractionNoItem");
            }
        }
    }

    IEnumerator DialogInteraction(SusToken.Token token)
    {
        ProgrammaticQuests.PhaseID currentPhase = GameManager.gameManager.phase;

        for (int i = 0; i < dialog.Length; i++)
        {
            AudioSource.clip = dialog[i].dialogAudio;
            subTitletext.text = dialog[i].dialogText;
            AudioSource.Play();

            yield return new WaitForSeconds(AudioSource.clip.length);


        }
        StartCoroutine("Leave", token);
    }

    IEnumerator DialogInteractionNoItem()
    {
        AudioSource.clip = noItemDialog.dialogAudio;
        subTitletext.text = noItemDialog.dialogText;
        AudioSource.Play();

        yield return new WaitForSeconds(AudioSource.clip.length);
        subTitletext.text = "";
    }

    IEnumerator Leave(SusToken.Token token)
    {
        AudioSource.clip = LeavingDialog.dialogAudio;
        subTitletext.text = LeavingDialog.dialogText;
        AudioSource.Play();

        float opacityMaterial = 1f;
        while (opacityMaterial > 0)
        {
            renderer.material.color = new Color(0, 0, 0, opacityMaterial);
            yield return new WaitForSeconds(.05f);
            opacityMaterial -= .1f;
        }

        yield return new WaitForSeconds(AudioSource.clip.length);
        subTitletext.text = "";
        GameManager.gameManager.TokenComplete(token);

        Destroy(gameObject);
    }
}
