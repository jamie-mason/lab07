using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Rotation
{
    private readonly float rotationX;
    private readonly float rotationY;
    private readonly float rotationZ;


    public Rotation(float rotationX, float rotationY, float rotationZ)
    {
        this.rotationX = rotationX;
        this.rotationY = rotationY;
        this.rotationZ = rotationZ;
    }
    public float getRotationX()
    {
        return rotationX;
    }
    public float getRotationY()
    {
        return rotationY;
    }
    public float getRotationZ()
    {
        return rotationZ;
    }
};
public class CarParachute : MonoBehaviour
{
    public GameObject car;
    Rigidbody rb;
    public GameObject parachute;
    [SerializeField] float MaxAirRollSpeed = 10f;
    [SerializeField] float RevertSpeed = 10f;
    [SerializeField] float AirRollMagnitude = 10f;
    [SerializeField] float ParachuteForwardSpeed = 10f;
    [SerializeField] float decel = 2f;
    [SerializeField] float paraDelay = 1f;
    float rotationX = 0f;
    float rotationY = 0f;
    float rotationZ = 0f;
    float avx = 0;
    float avy = 0;
    float avz = 0;
    float t = 0;
    [SerializeField] float cVal = 0f;


    [SerializeField] float dragForce = 0f;


    public cameraSwivel cam;
    [SerializeField] private CarIsGrounded carIsGrounded;

    private void Start()
    {
        rb = car.GetComponent<Rigidbody>();
        rotationY = car.transform.localEulerAngles.y;
        rb.drag = 0.01f;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.V) && !carIsGrounded.GroundCheck())
        {
            if (!parachuteIsActive())
            {
                rb.drag = 0f;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                StartCoroutine(parachuteDelay());
                rb.constraints = RigidbodyConstraints.None;
            }
            else
            {
                rb.drag = 0.01f;
                closeParachute();
            }
        }
       



    }
    private void FixedUpdate()
    {
        if (parachuteIsActive())
        {
            parachuteSteering();
            parachuteSpeed();
        }
        else
        {
            airRotation();
        }






    }
    IEnumerator parachuteDelay()
    {
        yield return new WaitForSeconds(1f);
        openParachute();
    }
    void parachuteSpeed()
    {

        float terminalVelocity;
        if (dragForce != 0f)
        {
            //terminal velocity if drag force is not equal to 0. and if the dragforce value is negative it sets teh value positive.
            terminalVelocity = -Physics.gravity.y / Mathf.Abs(dragForce);
        }
        else
        {
            //terminal velocity if drag force is equal to 0 because one cannot divide by 0.
            terminalVelocity = 0f;

        }
        //lift force when parachute is active
        float parMag = (rb.velocity.y + terminalVelocity * Mathf.Exp(-Time.fixedDeltaTime) - terminalVelocity);
        rb.AddForce(Vector3.down * parMag ,ForceMode.Acceleration);
        rb.velocity = new Vector3((ParachuteForwardSpeed + ParachuteForwardSpeed * car.transform.localRotation.x ) * -rb.transform.forward.x, rb.velocity.y, (ParachuteForwardSpeed + ParachuteForwardSpeed * car.transform.localRotation.x) * -rb.transform.forward.z);

    }
    void parachuteSteering()
    {
        
    

    }
    void revertRotation()
    {

    }
    
    bool checkKeyDown(KeyCode s)
    {
        if (Input.GetKeyDown(s) == true)
            return true;
        else 
            return false;
    }
    bool checkKeyUp(KeyCode s)
    {
        if (Input.GetKeyUp(s) == true)
            return true;
        else
            return false;
    }
    void airRotation()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.left * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
                
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.right * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;
        }

        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.Space))
        {

            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.up * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;
        }
        else if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Space))
        {

            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.down * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;


        }
        else if (Input.GetKey(KeyCode.J))
        {
            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.back * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;

        }
        else if (Input.GetKey(KeyCode.L))
        {

            if (rb.angularVelocity.magnitude < MaxAirRollSpeed)
            {
                rb.AddRelativeTorque(Vector3.forward * AirRollMagnitude * Time.deltaTime, ForceMode.Acceleration);
            }
            t = 0;
            avx = rb.angularVelocity.x;
            avy = rb.angularVelocity.y;
            avz = rb.angularVelocity.z;

        }
        
        if(!Input.GetKey(KeyCode.L) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.I))
        {
            //x angularVelocity deceleration
            if (rb.angularVelocity.x > 0f)
            {
                float val = Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avx) + cVal) / Mathf.Log(decel)))) - cVal;
                if (val > 0f)
                {
                    rb.angularVelocity = new Vector3(val, rb.angularVelocity.y, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = new Vector3(0f, rb.angularVelocity.y, rb.angularVelocity.z);
                }

            }
            else if(rb.angularVelocity.x < 0f)
            {
                float val = -Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avx) + cVal) / Mathf.Log(decel)))) + cVal;
                if (val < 0f)
                {
                    rb.angularVelocity = new Vector3(val, rb.angularVelocity.y, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = new Vector3(0f, rb.angularVelocity.y, rb.angularVelocity.z);
                }
            }

            //z angularVelocity deceleration
            if (rb.angularVelocity.z > 0f)
            {
                float val = Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avz) + cVal) / Mathf.Log(decel)))) - cVal;
                if (val > 0f)
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, val);
                }
                else
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0f);
                }

            }
            else if (rb.angularVelocity.z < 0f)
            {
                float val = -Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avz) + cVal) / Mathf.Log(decel)))) + cVal;
                if (val < 0f)
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, val);
                }
                else
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0f);
                }
            }
            // revert y axis
            if (rb.angularVelocity.y > 0f)
            {
                float val = Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avy) + cVal) / Mathf.Log(decel)))) - cVal;
                if (val > 0f)
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, val, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0f , rb.angularVelocity.z);
                }

            }
            else if (rb.angularVelocity.y < 0f)
            {
                float val = -Mathf.Pow(decel, -(t - (Mathf.Log(Mathf.Abs(avy) + cVal) / Mathf.Log(decel)))) + cVal;
                if (val > 0f)
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, val, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0f, rb.angularVelocity.z);
                }
            }

            t += Time.fixedDeltaTime;



        }
       
    }
    void revertRotation(Vector3 angVel)
    {
        
    }

    float revertRotationY(float rotation)
    {


        return rotation;
    }

    private void openParachute()
    {
        parachute.SetActive(true);
        
       


    }
  
    private void closeParachute()
    {
        parachute.SetActive(false);
    }
    private bool parachuteIsActive()
    {
        if (parachute.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    
}
