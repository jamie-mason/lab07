using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class cameraSwivel : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera AerialCamera;
    [SerializeField] CinemachineVirtualCamera GroundedCamera;

    private void Start()
    {

        CameraSwitcher.SwitchCamera(GroundedCamera);
        
    }
    private void OnEnable()
    {
        CameraSwitcher.Register(GroundedCamera);
        CameraSwitcher.Register(AerialCamera);
    }
    private void OnDisable()
    {
        CameraSwitcher.Unregister(GroundedCamera);
        CameraSwitcher.Unregister(AerialCamera);
    }
    public void changeCamera()
    {
        if (CameraSwitcher.IsActiveCamera(AerialCamera))
        {
            CameraSwitcher.SwitchCamera(GroundedCamera);
        }
        else
        {
            CameraSwitcher.SwitchCamera(AerialCamera);
        }
    }

    
    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
       
    }
    
}
