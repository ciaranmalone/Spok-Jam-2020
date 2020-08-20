using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x, z;
    Vector3 move;
    Vector3 velocity;
    private CharacterController controller;

    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float speed = 12f;

    [SerializeField]
    private float jumpHeight = 3f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;
    private float groundDistance = 0.4f;
    private bool isGrounded;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
