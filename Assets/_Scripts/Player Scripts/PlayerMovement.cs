using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isWalking = false;
    private float x, z;
    private Vector3 move;
    private Vector3 velocity;
    private CharacterController controller;

    //1.
    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float sprintSpeed = 15f;
    private float speed;
    private float stamina = 5f;
    [SerializeField] private float jumpHeight = 3f;

    //2.
    [Header("Ground Checking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.4f;
    public bool isGrounded;
    public bool crouched = false;
    private bool noSprintAllowed = false;

    //3.speed
    [Header("Input Controls")]
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode crouch = KeyCode.LeftControl;
    [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        speed = normalSpeed;
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

        if(Input.GetKey(jump) && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        if (x != 0 || z != 0)
        {
            controller.Move(move * speed * Time.deltaTime);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        controller.Move(velocity * Time.deltaTime);

        //crouching
        if(Input.GetKey(crouch)) { 
            crouched = true;
            controller.height = 1.9f;
            speed = crouchSpeed;
        } 
        //running
        else if(Input.GetKey(sprint) && stamina > 0 && !noSprintAllowed && isGrounded) {
            speed = sprintSpeed;

            stamina -= Time.deltaTime * 2;
        } 
        else {
            controller.height = 3.8f;
            speed = normalSpeed;
            crouched = false;
            noSprintAllowed = true;
        }

        if(stamina < 5f) {
            stamina += Time.deltaTime/2;
        } else {
            noSprintAllowed = false;
        }
       // print(stamina);
    }
}
