using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParachute : MonoBehaviour
{
    public GameObject car;
    public GameObject parachute;
    [SerializeField] private float ParachuteSpeed = -8f;
  
    [SerializeField]private float zLimit = 45f;
    [SerializeField] private float xLimitHigh = 45f;
    [SerializeField] private float xLimitLow = -45f;

    public cameraSwivel cam;

   

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            if (!parachuteIsActive())
            {
                cam.changeCamera();
                openParachute();

            }
            else
            {
                cam.changeCamera();
                closeParachute();
            }
        }
    }
    private void FixedUpdate()
    {

    }
    

    public void openParachute()
    {
        parachute.SetActive(true);

      

    }
    public void closeParachute()
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
