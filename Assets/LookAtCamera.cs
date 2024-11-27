using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera myCamera;
    // Update is called once per frame
    void Update()
    {
        if(myCamera == null)
        {
            myCamera = FindObjectOfType<Camera>();
        }
        if(myCamera != null)
        {
            transform.forward = myCamera.transform.forward;
        }
    }
}
