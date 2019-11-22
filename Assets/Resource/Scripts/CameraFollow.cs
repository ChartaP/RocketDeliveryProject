﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Camera MainCamera;
    private Vector3 CameraPos = new Vector3(0,2.2f,2f);
    private float Y_Dir = 0;
    private float X_Dir = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
            MainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
            return;
        
        MainCamera.transform.position = target.position;
        //target.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * 1.0f);
        MainCamera.transform.position -= Quaternion.Euler(0, target.eulerAngles.y, 0) * Vector3.forward * CameraPos.z ;
        MainCamera.transform.position += new Vector3(0,CameraPos.y,0);
        //MainCamera.transform.LookAt(target.position);

        Y_Dir -= Input.GetAxis("Mouse Y");
        X_Dir = target.eulerAngles.y;
        Debug.Log(X_Dir);
        MainCamera.transform.rotation = Quaternion.Euler(Y_Dir, X_Dir, 0);

    }

    public void SetPosition(float Height,float distance)
    {
        CameraPos.y = Height;
        CameraPos.z = distance;
    }
    
}