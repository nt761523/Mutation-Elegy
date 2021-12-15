using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    float rorationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    void Start()
    {
    }

    //private void LateUpdate()
    //{
    //    CamControl();
    //}
    public void CamControl(bool onoff)
    {
        if (onoff) 
        { 
            mouseX += Input.GetAxis("Mouse X") * rorationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rorationSpeed;
            //mouseY = Mathf.Clamp(mouseY, -35, 60);
            mouseY = Mathf.Clamp(mouseY, -15, 35);
        }


        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
