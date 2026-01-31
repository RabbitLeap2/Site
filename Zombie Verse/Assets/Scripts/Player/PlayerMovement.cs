using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private float speed;
    public float walkspeed;
    public float sprintspeed;
    public float dashspeed;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Animator anim;
    public  Vector3 velocity;
    public Vector3 move;
    public GameObject pernas;

    public float crouchSpeed;
    private float AlturaAtual;
    public float AlturaNova; 
    private float CentroAtual;
    public float CentroNovo;

    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public bool isGrounded;
    bool isMoving;
   public bool canMove;
    public bool isDashing;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    public MovementState state;

    public static PlayerMovement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public enum MovementState
    {
        walking,
        crouching,
        dashing,
        sprinting,
        air
    }
    private void StateHandler()
    {
       


        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            speed = crouchSpeed;
        }
        if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            speed = sprintspeed;
        }
        else if (isGrounded)
        {
            state = MovementState.walking;
            speed = walkspeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        canMove = true;
        anim = pernas.GetComponent<Animator>();
        AlturaAtual = controller.height;
        CentroAtual = controller.center.y;

    }



    // Update is called once per frame
    void Update()
    {
       
        if (canMove)
        {
            StateHandler();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            Vector3 forward = cameraTransform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = cameraTransform.right;
            right.y = 0;
            right.Normalize();

            // move
            move = right * x + forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (lastPosition != gameObject.transform.position && isGrounded == true)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            lastPosition = gameObject.transform.position;
            if (Input.GetKeyDown(crouchKey))
            {
                anim.SetBool("Agachado", true);
                controller.center = new Vector3(0, CentroNovo, 0);

                controller.height = AlturaNova;
            }
            if (Input.GetKeyUp(crouchKey))
            {
                controller.height = AlturaAtual;
                anim.SetBool("Agachado", false);
                controller.center = new Vector3(0, CentroAtual, 0);
            }
            if (!isDashing)
            {
                controller.Move(move * speed * Time.deltaTime);
            }
        }



    }
}
