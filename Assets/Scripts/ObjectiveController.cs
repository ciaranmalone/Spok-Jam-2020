using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private int objective;
    [SerializeField] private string objectiveText;
    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    private TextMeshProUGUI m_Text;
    void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Text.text = objectiveText;

        GameEvents.current.onObjectiveComplete += ObjectiveCompleteHandler;
    }

    private void ObjectiveCompleteHandler(int objective) 
    {
        if(objective == this.objective) {
            audioSource.PlayOneShot(clip, .1f);
            m_Text.fontStyle = FontStyles.Strikethrough;
        }
    }
}
