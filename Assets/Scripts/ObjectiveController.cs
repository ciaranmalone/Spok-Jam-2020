using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private int objective;
    [SerializeField] private string objectiveText;
    private TextMeshProUGUI m_Text;
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Text.text = objectiveText;

        GameEvents.current.onObjectiveComplete += ObjectiveCompleteHandler;
    }

    private void ObjectiveCompleteHandler(int objective) 
    {
        if(objective == this.objective) {
            m_Text.text = "complete";
        }
    }
}
