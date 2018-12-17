using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{

    float X;
    float Y;
    public float sensi;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float peek;

    [HideInInspector] public float recoil;
    float rotX;
    float rotY;

    void Start()
    {

    }

    void Update()
    {

        X += Input.GetAxis("Mouse X") * GameObject.FindObjectOfType<Controls>().mouseSensitivityX;
        Y += Input.GetAxis("Mouse Y") * GameObject.FindObjectOfType<Controls>().mouseSensitivityY;

        //Recoil and peek.
        {
            /*  
            if (Input.GetKeyDown(GameObject.FindObjectOfType<Controls>().Fire))
             {
                 Y += recoil;
             }

             if (Input.GetKey(GameObject.FindObjectOfType<Controls>().PeekRight))
             {
                 peek = -10f;
             }
             else if (Input.GetKey(GameObject.FindObjectOfType<Controls>().PeekLeft))
             {
                 peek = 10f;
             }
             else
             {
                 peek = 0;
             } 
             */
        }

        rotX = ClampAngle(X, minimumX, maximumX);
        rotY = ClampAngle(Y, minimumY, maximumY);

        this.transform.localEulerAngles = new Vector3(-rotY, rotX, peek);

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}