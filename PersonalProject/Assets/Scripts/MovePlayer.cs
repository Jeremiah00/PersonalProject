using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    //Stuff for checking the ground 
    public float Playerheight;
    public float groundDrag;
    public bool grounded;
    public LayerMask Ground;

    //For my inputs
    float horziontalInput;
    float verticalInput;

    Rigidbody rb;
    public Transform orientation;
    public float moveSpeed;
    public float runningSpeed;
    public float walkingSpeed;
    
    Vector3 moveDirection;
    public float JumpForce;
    public float airMultiplier;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fires raycast down to find if we are grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, Playerheight * 0.5f + 0.2f, Ground);

        horziontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        SpeedLimit();

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();
            //grounded = false;


        if (grounded)
        {
            
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    void FixedUpdate()
    {
        Movement();
        MoveState();
    }

    void Movement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horziontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    void SpeedLimit()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if( flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void MoveState()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = runningSpeed;

        else
            moveSpeed = walkingSpeed;
    }
    


}
