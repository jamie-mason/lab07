using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Rotation {
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
    [SerializeField] float RotationSpeed = 10f;
    [SerializeField] float RevertSpeed = 10f;
    [SerializeField] float AirRollSpeed = 10f;
    float rotationX = 0f;
    float rotationY = 0f;
    float rotationZ = 0f;
   
    [SerializeField] float dragForce = 0f;
    float old;


    public cameraSwivel cam;
    [SerializeField] private CarIsGrounded carIsGrounded;

    private void Start()
    {
        rb = car.GetComponent<Rigidbody>();
        old = car.GetComponent<Rigidbody>().velocity.y;

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.V) && !carIsGrounded.GroundCheck())
        {
            if (!parachuteIsActive())
            {
               
                openParachute();
                
            }
            else
            {
                closeParachute();
            }
        }
       



    }
    private void FixedUpdate()
    {
        if (!carIsGrounded.GroundCheck())
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
       





    }
    void parachuteSpeed()
    {
        Rigidbody rb = car.GetComponent<Rigidbody>();

        float terminalVelocity;
        if (dragForce != 0f)
        {
            terminalVelocity = -Physics.gravity.y / dragForce;
        }
        else
        {
            terminalVelocity = 0f * (Mathf.Abs(Physics.gravity.y) + Physics.gravity.y * Time.fixedDeltaTime);

        }
        rb.AddForce(Vector3.down * (rb.velocity.y + terminalVelocity * Mathf.Exp(-Time.fixedDeltaTime) - terminalVelocity),ForceMode.Acceleration);
       
        Debug.Log(terminalVelocity);
    }
    void parachuteSteering()
    {
        var rot = car.transform.localEulerAngles;       
        if (rotationX >= -12f && rotationX <= 12f)
        {
            if (Input.GetKey(KeyCode.I))
            {
                rotationX -= RotationSpeed * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.K))
            {
                rotationX += RotationSpeed * Time.deltaTime;
            }
            else
            {
                rotationX = revertRotation(rotationX);
            }
            rotationX = Mathf.Clamp(rotationX, -12f, 12f);
        }
        else
        {
            rotationX = revertRotation(rotationX);

        }
        if (rotationX >= -12f && rotationX <= 12f)
        {
            if (Input.GetKey(KeyCode.J))
            {
                setVelocityDirection();
                rotationZ -= RotationSpeed * Time.deltaTime;
                rotationY -= RotationSpeed * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.L))
            {
                setVelocityDirection();
                rotationZ += RotationSpeed * Time.deltaTime;
                rotationY += RotationSpeed * Time.deltaTime;
            }
            else
            {
                rotationZ = revertRotation(rotationZ);
            }
            rotationZ = Mathf.Clamp(rotationZ, -12f, 12f);
        }
        else
        {
            rotationZ = revertRotation(rotationZ);

        }

        Rotation rotation = new Rotation(rotationX, rotationY, rotationZ);
        rot.x = rotation.getRotationX();
        rot.y = rotation.getRotationY();
        rot.z = rotation.getRotationZ();
        car.transform.localEulerAngles = rot;

    }
    void setVelocityDirection()
    {
        float mag = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2f) + Mathf.Pow(rb.velocity.x, 2f));
        Vector3 direction = new Vector3(mag * Mathf.Sin(car.transform.localRotation.y) * Mathf.Sin(car.transform.localRotation.y), 0f, mag * Mathf.Cos(car.transform.localRotation.y) * Mathf.Sin(car.transform.localRotation.y));
        rb.AddForce(direction, ForceMode.Force);
    }
    void airRotation()
    {
        Vector3 rot = car.transform.localEulerAngles;
        if (Input.GetKey(KeyCode.I))
        {
            rotationX -= AirRollSpeed * Time.deltaTime;
            rot.x = rotationX;
            car.transform.localEulerAngles = rot;

        }
        else if (Input.GetKey(KeyCode.J))
        {
            rotationX += AirRollSpeed * Time.deltaTime;
            rot.x = rotationX;
            car.transform.localEulerAngles = rot;
        }
        if (Input.GetKey(KeyCode.J))
        {
            rotationZ -= AirRollSpeed * Time.deltaTime;
            rot.z = rotationZ;
            car.transform.localEulerAngles = rot;

        }
        else if (Input.GetKey(KeyCode.L))
        {
            rotationZ += AirRollSpeed * Time.deltaTime;
            rot.z = rotationZ;
            car.transform.localEulerAngles = rot;
        }
        
       
    }

    float revertRotationY(float rotation)
    {


        return rotation;
    }
    float revertRotation(float rotation)
    {
        if (rotation > 0f)
        {
            if (rotation - RevertSpeed * Time.fixedDeltaTime < 0f)
            {
                rotation -= (rotation % RevertSpeed) * RevertSpeed * Time.fixedDeltaTime;
            }
            else
            {
                rotation -= RevertSpeed * Time.fixedDeltaTime;
            }
        }
        else if (rotation < 0f)
        {
            if (rotation + RevertSpeed * Time.fixedDeltaTime > 0f)
            {
                rotation -= (rotation % RevertSpeed) * RevertSpeed * Time.fixedDeltaTime;
            }
            else
            {
                rotation += RevertSpeed * Time.fixedDeltaTime;
            }
        }
        return rotation;

    }
    float velocityFactor()
    {
        // Get and return the magnitude of the plane
        return car.GetComponent<Rigidbody>().velocity.magnitude;
    }
    private void openParachute()
    {
        parachute.SetActive(true);
        
       


    }
    private void lowerSpeed()
    {
        
        

    }
    private void parachuteControls()
    {
      
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
