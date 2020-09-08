using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public CharacterController player;
    public Transform reciever;

    bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            print("it's gaming time my guy");

            print("it's dot product time");
            float rotationDifference = Quaternion.Angle(transform.rotation, reciever.rotation);
            rotationDifference += 180;
            player.transform.Rotate(Vector3.up, rotationDifference);

            Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
            player.transform.position = reciever.position + positionOffset;
            playerIsOverlapping = false;

            /*if (dotProduct < 0f)
            {
                print("it's dot product time");
                float rotationDifference = Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDifference += 180;
                player.transform.Rotate(Vector3.up, rotationDifference);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
                player.transform.position = reciever.position + positionOffset;
                playerIsOverlapping = false;
            }*/
        }
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            //playerIsOverlapping = true;
            teleport();
            print("it's gaming time");
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

    void teleport()
    {
        Vector3 portalToPlayer = player.transform.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
        print("it's gaming time my guy");

        print("it's dot product time");
        float rotationDifference = Quaternion.Angle(transform.rotation, reciever.rotation);
        rotationDifference += 180;
        player.transform.Rotate(Vector3.up, rotationDifference);

        Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
        player.transform.position = reciever.position + positionOffset;
    }
}
