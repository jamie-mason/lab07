using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{

    [SerializeField] private WheelCollider FL;
    [SerializeField] private WheelCollider FR;
    [SerializeField] private WheelCollider RL;
    [SerializeField] private WheelCollider RR;


    [SerializeField] private Transform FLMesh;
    [SerializeField] private Transform FRMesh;
    [SerializeField] private Transform RLMesh;
    [SerializeField] private Transform RRMesh;

    [SerializeField] private CarIsGrounded carIsGrounded;

    [SerializeField] private Rigidbody rb;
    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakingForce = 0f;
    private float currentTurnAngle = 0f;
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {


        if (rb.velocity.magnitude < 0.1f)
        {
            rb.velocity = Vector3.zero;
        }
        float vert = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentBreakingForce = breakingForce;
            currentAcceleration = 0f;
        }
        else
        {
            currentBreakingForce = 0f;
        }
        if (carIsGrounded.GroundCheck()) { 
        currentAcceleration = acceleration * -vert;
        }
        

        FL.motorTorque = currentAcceleration;
        FR.motorTorque = currentAcceleration;
        

        FL.brakeTorque = currentBreakingForce;
        FR.brakeTorque = currentBreakingForce;
        RL.brakeTorque = currentBreakingForce;
        RR.brakeTorque = currentBreakingForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        FL.steerAngle = currentTurnAngle;
        FR.steerAngle = currentTurnAngle;
        
       


        //updateWheel(FL, FLMesh);
        //updateWheel(FR, FRMesh);
        //updateWheel(RL, RLMesh);
        //updateWheel(RR, RRMesh);
    }
    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);
    }
    public float getCurrentAcceleration()
    {
        return currentAcceleration;
    }
    public float getCurrentBreakForce()
    {
        return currentBreakingForce;
    }
    public float getCurrentTurnAngle()
    {
        return currentTurnAngle;
    }

    void updateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);
        trans.position = position;
        trans.rotation = rotation;
    }
    public WheelCollider getFL()
    {
        return FL;

    }
    public WheelCollider getRR()
    {
        return RR;

    }
    public WheelCollider getFR()
    {
        return FR;

    }
    public WheelCollider getRL()
    {
        return RL;

    }
    public Rigidbody getCar()
    {
        return rb;
    }
}
