using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    // Start is called before the first frame update

    float heightOffGround;
    float horziontalInput;
    float verticalInput;

    public bool RightWall;
    public bool LeftWall;
    public LayerMask Ground;
    public LayerMask Wall;
    RaycastHit rightWallhit;
    RaycastHit leftWallhit;
    Rigidbody rb;
    public MovePlayer mp;
    public Transform oreintation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mp = GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckWalls()
    {
        RightWall = Physics.Raycast(transform.position, Vector3.right, out rightWallhit, 0.7f, Wall);
        LeftWall = Physics.Raycast(transform.position, Vector3.left, out leftWallhit, 0.7f, Wall);
    }
    bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, heightOffGround, Ground);
    }

    void CanWallRun()
    {
        if((LeftWall || RightWall) && AboveGround())
        {
            StartWallRun();
        }

        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        mp.wallRuning = true;
    }

    void WallRunMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = RightWall ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        rb.AddForce(wallForward * 10f, ForceMode.Force);
    }

    void StopWallRun()
    {

    }
}
