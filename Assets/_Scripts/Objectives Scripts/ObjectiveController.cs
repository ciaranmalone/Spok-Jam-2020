using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    private int objective;
    //[SerializeField] private string objectiveText;
    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    private TextMeshProUGUI m_Text;
    void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        m_Text = GetComponent<TextMeshProUGUI>();
        //m_Text.text = objectiveText;

        GameEvents.current.onObjectiveComplete += ObjectiveCompleteHandler;
    }

    private void ObjectiveCompleteHandler(int objective) 
    {
        if(objective == this.objective) {
            audioSource.PlayOneShot(clip, .1f);
            m_Text.fontStyle = FontStyles.Strikethrough;
            GameEvents.current.onObjectiveComplete -= ObjectiveCompleteHandler;
        }
    }

    public void setObjectiveOffset(int offset)
    {
        objective = offset;
    }

    public void setAudioclip(AudioClip clip)
    {
        this.clip = clip;
    }
}
