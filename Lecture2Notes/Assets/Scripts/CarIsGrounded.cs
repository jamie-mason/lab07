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
    public bool GroundCheck()
    {
        if (carController.getFL().isGrounded || carController.getFR().isGrounded || carController.getRL().isGrounded || carController.getRR().isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void FixedUpdate()
    {
       
       
     
       
    }
    void Update()
    {
        
    }
}
