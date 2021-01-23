using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    PlayerManager manager;

    public float moveSpeed = 8f, runSpeed = 1.8f, crouchMultiply = 0.75f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float pushForce = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    public Vector3 velocity;
    bool isGrounded;
    float xAxis;
    float zAxis;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GetComponent<PlayerManager>();
        manager.enumManager.moveState = PlayerMoveState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.canMove)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded)
        {
            velocity.y -= 2;
        }
         xAxis = Input.GetAxis("Horizontal");
         zAxis = Input.GetAxis("Vertical");

        move = transform.right * xAxis + transform.forward * zAxis;

        if (Input.GetButton("Fire3"))
        {
            //juostaan
            controller.Move(move * moveSpeed * runSpeed * Time.deltaTime);
            manager.enumManager.moveState = PlayerMoveState.Run;
        }
        else
        {
            //kävellään
            if (manager.enumManager.standState == PlayerStandState.Crouch)
            {
                controller.Move(move * (crouchMultiply * moveSpeed) * Time.deltaTime);
            }
            else
            {
                controller.Move(move * moveSpeed * Time.deltaTime);
            }
            
            manager.enumManager.moveState = PlayerMoveState.Walk;
        }

        //jos ollaan paikoillaan niin isketään idle
        if(move == Vector3.zero)
        {
            manager.enumManager.moveState = PlayerMoveState.Idle;
        }

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

  
            velocity.y += gravity * Time.deltaTime;
        
        
        controller.Move(velocity * Time.deltaTime);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.layer == 8)
        {
            return;
        }

        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            return;
        }
        rb.AddForce(move * pushForce);


       
    }
}
