using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    private ProgrammaticQuests.PhaseID startPhase;

    [SerializeField]
    GameObject minute, hour;
    private static float angleMinute=0, angleHour=330;
    private static ClockController timeLord; //dr hu??
    private float localMinute, localHour;

    private void Awake()
    {
        if (!timeLord) timeLord = this;
    }

    void Start()
    {
        ApplyCurrentRotation();
        startPhase = GameManager.gameManager.phase;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLord==this && startPhase != GameManager.gameManager.phase)
        {
            angleMinute += angleMinute > 360 ? -330 : 30;
            angleHour += angleHour > 360 ? -357.5f : 2.5f;//2.5 degrees of 5 min of an hr
            
            ApplyCurrentRotation();

            startPhase = GameManager.gameManager.phase;
        }
        else if(localMinute != angleMinute)
        {
            ApplyCurrentRotation();
        }
    }

    void ApplyCurrentRotation()
    {
        localMinute = angleMinute;
        localHour = angleHour;
        Vector3 temp = minute.transform.localRotation.eulerAngles;
        minute.transform.localRotation = Quaternion.Euler(new Vector3(temp.x, localMinute, temp.z));

        temp = hour.transform.localRotation.eulerAngles;
        hour.transform.localRotation = Quaternion.Euler(new Vector3(temp.x, localHour, temp.z));
    }

    private void OnDisable()
    {
        ///IF YOU DARE TO DISABLE CLOCKS DURING THE GAME I SWEAR I'M GOING TO ASSIGN A NEW ONE AND YOU WON'T BE HAPPY >:(
        if (timeLord == this) timeLord = null;

    }

}
