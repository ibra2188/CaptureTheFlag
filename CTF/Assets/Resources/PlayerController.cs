using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField]
    GameObject cameraHolder;

    private bool grounded;
    private Vector3 smoothMoveVelocity;
    private float verticalLookRotation; 
    private Vector3 moveAmount;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Look();
        Move();
        Jump();
    }
    
    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir* (Input.GetKey(KeyCode.LeftShift)? sprintSpeed: walkSpeed), ref smoothMoveVelocity, smoothTime);
        
    }

    void Jump()
    { 
        if(Input.GetKeyDown(KeyCode.Space)  && grounded)
        { rb.AddForce(transform.up * jumpForce); }
        
    }

    public void SetGrounded(bool _grounded)
    { grounded = _grounded; }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount)* Time.fixedDeltaTime);
    }
}
