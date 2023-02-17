using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarIsGrounded : MonoBehaviour
{
    public float distanceToGround = 1f;
    [SerializeField] CarController carController;
    void Start()
    {
        
    }
   /* public bool GroundCheck()
    {
        Rigidbody rb = carController.getCar();
        *//*if (rb.SweepTest(transform.position, Vector3.down, distanceToGround +0.1f))
        {
            Debug.Log("Grounded");
            return true;
        }
        else
        {
            Debug.Log("Not Grounded");
            return false;
        }*//*
    }*/
    private void FixedUpdate()
    {
    }
    void Update()
    {
        
    }
}
