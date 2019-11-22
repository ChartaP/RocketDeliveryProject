﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private Rigidbody CarRigidbody;
    [SerializeField]
    private WheelCollider FRightWheel;
    [SerializeField]
    private WheelCollider FLeftWheel;
    [SerializeField]
    private WheelCollider BRightWheel;
    [SerializeField]
    private WheelCollider BLeftWheel;

    [SerializeField]
    private List<Transform> WheelMesh = new List<Transform>();

    enum eTransmission
    {
        P = 1,
        N = 2,
    }

    private eTransmission curTrans;

    [SerializeField]
    private bool bSideGear = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if(CarRigidbody == null)
        {
            CarRigidbody = transform.GetComponent<Rigidbody>();
        }
        curTrans = eTransmission.P;
        CarUnactivate();

    }

    // Update is called once per frame
    void Update()
    {
        if (WheelMesh.Count > 0)
        {
            Vector3 pos;
            Quaternion quat;
            FRightWheel.GetWorldPose(out pos, out quat);
            WheelMesh[0].position = pos;
            WheelMesh[0].rotation = quat;
            FLeftWheel.GetWorldPose(out pos, out quat);
            WheelMesh[1].position = pos;
            WheelMesh[1].rotation = quat;
            BRightWheel.GetWorldPose(out pos, out quat);
            WheelMesh[2].position = pos;
            WheelMesh[2].rotation = quat;
            BLeftWheel.GetWorldPose(out pos, out quat);
            WheelMesh[3].position = pos;
            WheelMesh[3].rotation = quat;
        }
    }

    public void Accel(float fSpeed)
    {
        if(curTrans == eTransmission.P)
        {
            ChangeTrans(eTransmission.N);
        }
        Debug.Log(Vector3.forward * fSpeed );
        //CarRigidbody.AddForce(Vector3.forward * fSpeed );
        BRightWheel.motorTorque = fSpeed *20;
        BLeftWheel.motorTorque = fSpeed * 20;
    }

    public void Break(float fSpeed)
    {
        if (CarRigidbody.velocity.z > 0.1f || CarRigidbody.velocity.z < -0.1f)
        {
            //CarRigidbody.AddForce(Vector3.forward * fSpeed );
            BRightWheel.brakeTorque = fSpeed;
            BLeftWheel.brakeTorque = fSpeed;
        }
        else
        {
            ChangeTrans(eTransmission.P);
        }
    }

    public void Handle(float fDir)
    {
        FRightWheel.steerAngle = fDir * 2000;
        FLeftWheel.steerAngle = fDir * 2000;
    }

    private void ChangeTrans(eTransmission eTrans)
    {
        if (curTrans == eTrans)
            return;
        switch (curTrans)
        {
            case eTransmission.N:
                break;
            case eTransmission.P:
                break;
        }
        curTrans = eTrans;
        switch (curTrans)
        {
            case eTransmission.N:
                break;
            case eTransmission.P:
                break;
        }
    }

    public void CarActivate()
    {
        bSideGear = false;
        FRightWheel.brakeTorque = 0;
        FLeftWheel.brakeTorque = 0;
        BRightWheel.brakeTorque = 0;
        BLeftWheel.brakeTorque = 0;
    }

    public void CarUnactivate()
    {
        bSideGear = true;
        FRightWheel.brakeTorque = 100f;
        FLeftWheel.brakeTorque = 100f;
        BRightWheel.brakeTorque = 100f;
        BLeftWheel.brakeTorque = 100f;
    }
}