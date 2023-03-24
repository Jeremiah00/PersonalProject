using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    float horziontalInput;
    float verticalInput;

    Rigidbody rb;
    public Transform orientation;
    public float moveSpeed;

    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horziontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horziontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);
    }

    


}
