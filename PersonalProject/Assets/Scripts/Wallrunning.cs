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

    }

    void WallRunMovement()
    {

    }

    void StopWallRun()
    {

    }
}
