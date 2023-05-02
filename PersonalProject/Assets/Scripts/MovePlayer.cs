using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    //Stuff for checking the ground 
    public float Playerheight;
    public float groundDrag;
    bool grounded;
    public LayerMask Ground;
    public LayerMask Ceiling;
    bool ceiling;

    public TextMeshProUGUI speedText;

    //For my inputs
    float horziontalInput;
    float verticalInput;

    Rigidbody rb;
    public Transform orientation;
    private float moveSpeed;
    public float runningSpeed;
    public float walkingSpeed;
    
    Vector3 moveDirection;
    public float JumpForce;
    public float airMultiplier;
    public float crounchSpeed;
    float startYScale;
    float crounchYScale;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        startYScale = transform.localScale.y;
        crounchYScale = startYScale / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Fires raycast down to find if we are grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, Playerheight * 0.5f + 0.2f, Ground);
        ceiling = Physics.Raycast(transform.position, Vector3.up, Playerheight * 0.5f + 0.2f, Ceiling);
        horziontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        SpeedTracker();
        SpeedLimit();

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();
            //grounded = false;

        if(Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, crounchYScale, transform.localScale.z);
            moveSpeed = crounchSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.C))
        {
            if(!ceiling)
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            moveSpeed = walkingSpeed;
        }

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

    void SpeedTracker()
    {
        speedText.text = "Speed " + moveSpeed;
    }
    


}
