using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;
    public Transform test;

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
            //teleport();
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
        /*Vector3 portalToPlayer = player.transform.position - transform.position;
        float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
        print("it's gaming time my guy");

        print("it's dot product time");
        float rotationDifference = Quaternion.Angle(transform.rotation, reciever.rotation);
        rotationDifference += 180;
        player.transform.Rotate(Vector3.up, rotationDifference);

        Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
        player.transform.position = reciever.position + positionOffset;*/

        //player.gameObject.GetComponent<CharacterController>().enabled = false;
        
        player.position = reciever.position;
        //Physics.Simulate(1);

        //test.position = reciever.position;
        //player.position = new Vector3(0, 0, 0);

        StartCoroutine(check());

        /*if (player.gameObject.transform.position == reciever.position)
        {
            print(player.transform.position + " hello");
            print(reciever.position + " hello");
        }*/
    }

    IEnumerator check()
    {
        reciever.gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSecondsRealtime(0.3f);

        reciever.gameObject.GetComponent<BoxCollider>().enabled = true;

        //player.gameObject.GetComponent<CharacterController>().transform.position = reciever.position;
        //player.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
