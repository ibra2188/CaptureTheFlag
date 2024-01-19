using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerController.gameObject) 
            return;
        
        playerController.SetGrounded(true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == playerController.gameObject) return;
        playerController.SetGrounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == playerController.gameObject) return;
        
        playerController.SetGrounded(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerController.gameObject) return;
        
        playerController.SetGrounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == playerController.gameObject) return;
        
        playerController.SetGrounded(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == playerController.gameObject) return;
        
        playerController.SetGrounded(true);
    }
}
