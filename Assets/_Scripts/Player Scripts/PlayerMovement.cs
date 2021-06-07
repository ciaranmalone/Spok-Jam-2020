using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool isWalking = false;
    private float x, z;
    private Vector3 move;
    private Vector3 velocity;
    private CharacterController controller;

    [Header("Object Dependencies")]
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask roofMask;
    [SerializeField] private Transform headCheck;
    private float roofDistance = 0.4f;
    public bool spaceAbove;

    //1. players physics satttributes
    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float crouchHeight = 2f;
    [SerializeField] private float normalHeight = 3.8f;
    [SerializeField] private float jumpHeight = 3f;
    private float speed;
    private float stamina = 5f;
    
    //2. 
    private float groundDistance = 0.4f;
    public bool isGrounded;
    public bool crouched = false;
    private bool noSprintAllowed = false;

    //3. Player controls
    [Header("Input Controls")]
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode crouch = KeyCode.LeftControl;
    [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

    [Header("PlayerIndicator")]
    private GameObject crouchIndicator;
    private GameObject sprintIndicator;
    private Graphic sprintIndicatorGraphic;
    [SerializeField] private Color sprintOn;
    [SerializeField] private Color sprintOff;

    private bool jumpActive = false;
    private bool crouchActive = false;
    private bool sprintActive = false;

    void Start()
    {
        crouchIndicator = IndicatorSingletons.crouchIndicatorSingleton;
        sprintIndicator = IndicatorSingletons.sprintIndicatorSingleton;

        crouchIndicator.SetActive(false);
        controller = gameObject.GetComponent<CharacterController>();
        speed = normalSpeed;

        sprintIndicatorGraphic = sprintIndicator.GetComponent<Graphic>();
    }
    private void Update()
    {
        crouchIndicator = IndicatorSingletons.crouchIndicatorSingleton;
        sprintIndicator = IndicatorSingletons.sprintIndicatorSingleton;
        
        sprintIndicatorGraphic = sprintIndicator.GetComponent<Graphic>();
        
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        jumpActive = Input.GetKey(jump);
        crouchActive = Input.GetKey(crouch);
        sprintActive = Input.GetKey(sprint);
    }
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        spaceAbove = Physics.CheckSphere(headCheck.position, roofDistance, groundMask);        

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        
        move = transform.right * x + transform.forward * z;

        if(jumpActive && isGrounded && !spaceAbove) {
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
        if(crouchActive) {
            crouchIndicator.SetActive(true);
            
            crouched = true;
            controller.height = crouchHeight;
            cameraHead.transform.localPosition = new Vector3(0, 0.6f, 0);
            speed = crouchSpeed;
        } 
        //running
        else if(sprintActive && stamina > 0 && !noSprintAllowed && isGrounded && (x+z) > 0.1) {
            speed = sprintSpeed;
            stamina -= Time.deltaTime * 2;
            sprintIndicatorGraphic.color = sprintOn;
            sprintIndicator.SetActive(true);
        }
        else if(!spaceAbove) {
            crouchIndicator.SetActive(false);

            controller.height = normalHeight;
            cameraHead.transform.localPosition = new Vector3(0, 1.6f, 0);
            speed = normalSpeed;
            crouched = false;
        
           if(stamina < 0) noSprintAllowed = true;
            
        }

        if (stamina < 5f) {
            stamina += Time.deltaTime/2;
            sprintIndicator.SetActive(true);

            if(noSprintAllowed) sprintIndicatorGraphic.color = sprintOff;

        }
        else if(!sprintActive) {
            noSprintAllowed = false;
            sprintIndicator.SetActive(false);
        }
        sprintIndicator.transform.localScale = new Vector3(stamina/2,.3f,1);
    }
}