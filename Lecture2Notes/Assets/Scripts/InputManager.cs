using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }
}
