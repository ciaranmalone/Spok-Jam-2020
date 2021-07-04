using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongFieldsPhoneHandler : MonoBehaviour
{
    [SerializeField] private int x, z, maxStep = 3;
    [SerializeField] GameObject phone;
    [SerializeField] Transform North, South, East, West;

    private AudioSource audioSource;
    float northSouth;
    float eastWest;
    void Start()
    {
        audioSource = phone.GetComponentInChildren<AudioSource>();
        northSouth = Vector3.Distance(North.position, South.position);
        eastWest = Vector3.Distance(East.position, West.position);
        print($"phone Pos: {phone.transform.position}");

        phone.transform.position += new Vector3(eastWest * x, 0, northSouth * z);
        print($"phone Pos: {phone.transform.position}");

        audioSource.maxDistance =  Mathf.Sqrt(Mathf.Pow(x* eastWest, 2) + Mathf.Pow(z* northSouth, 2)) + 69;

    }
    internal void MovePhone(LoopPlayerAround.dir dir)
    {
        switch (dir)
        {
            case LoopPlayerAround.dir.NORTH:
                if(z<-maxStep) return;
                phone.transform.position += new Vector3(0, 0, -northSouth);
                z--;
                break;

            case LoopPlayerAround.dir.SOUTH:
                if(z>maxStep) return;
                phone.transform.position += new Vector3(0, 0, northSouth);
                z++;
                break;

            case LoopPlayerAround.dir.EAST:
                if(x<-maxStep) return;
                phone.transform.position += new Vector3(-eastWest, 0, 0);
                x--;
                break;

            case LoopPlayerAround.dir.WEST:
                if(x>maxStep) return;
                phone.transform.position += new Vector3(eastWest, 0, 0);
                x++;
                break;

            default:
                //no
                break;
        }

        audioSource.maxDistance =  Mathf.Sqrt(Mathf.Pow(x* eastWest, 2) + Mathf.Pow(z* northSouth, 2)) + 69;

    }
}
