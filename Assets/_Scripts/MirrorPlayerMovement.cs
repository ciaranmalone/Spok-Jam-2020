using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayerMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(player.position.x * -1 + offset.x, player.position.y+offset.y, player.position.z+ offset.z);
        transform.rotation = Quaternion.Euler(player.rotation.eulerAngles.x,  player.rotation.eulerAngles.y*-1, player.rotation.eulerAngles.z);
    }
}
