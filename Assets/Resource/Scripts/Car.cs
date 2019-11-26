using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCarType
{
    Close = 0,
    Open = 1
}

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
    private Transform CarSeat;
    [SerializeField]
    private bool isEnter = false;
    public bool IsEnter { get { return isEnter; } }
    [SerializeField]
    private Transform passenger= null;


    [SerializeField]
    private List<Transform> WheelMesh = new List<Transform>();
    [SerializeField]
    private eCarType eType = eCarType.Close;
    

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
        if (CarRigidbody == null)
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
        //CarRigidbody.AddForce(transform.up * -fSpeed *10f  );
        float Boost = 1f;
        

        BRightWheel.motorTorque = fSpeed * 15f * Boost;
        BLeftWheel.motorTorque = fSpeed * 15f * Boost;
    }

    public void Break(float fSpeed)
    {
        if (CarRigidbody.velocity.magnitude > 1.0f)
        {
            BRightWheel.brakeTorque = fSpeed;
            BLeftWheel.brakeTorque = fSpeed;
        }
        else
        {
            curTrans = eTransmission.P;
        }
    }

    public void Steering(float fDir)
    {
        FRightWheel.steerAngle = fDir * 40;
        FLeftWheel.steerAngle = fDir * 40;
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

    private void CarActivate()
    {
        FRightWheel.brakeTorque = 0;
        FLeftWheel.brakeTorque = 0;
        BRightWheel.brakeTorque = 0;
        BLeftWheel.brakeTorque = 0;
    }

    private void CarUnactivate()
    {
        FRightWheel.brakeTorque = 100f;
        FLeftWheel.brakeTorque = 100f;
        BRightWheel.brakeTorque = 100f;
        BLeftWheel.brakeTorque = 100f;
    }

    public Vector3 SeatPos()
    {
        return CarSeat.position;
    }

    public void CarEnter(Transform passenger)
    {
        if (isEnter)
            return;
        CarActivate();
        transform.tag = passenger.tag;
        passenger.parent = CarSeat;
        passenger.position = CarSeat.position;
        passenger.rotation = CarSeat.rotation;
        ObjectCtrl ctrl = passenger.GetComponent<ObjectCtrl>();
        ctrl.Enter();
        if(eType == eCarType.Close)
            passenger.GetComponent<CharacterController>().enabled = false;
        this.passenger = passenger;
        isEnter = true;
    }

    public void CarExit()
    {
        if (!isEnter)
            return;
        CarUnactivate();
        transform.tag = "Car";
        passenger.parent = this.transform.parent;
        passenger.transform.position += new Vector3(-2,1,0);
        ObjectCtrl ctrl = passenger.GetComponent<ObjectCtrl>();
        ctrl.Exit();
        if (eType == eCarType.Close)
            passenger.GetComponent<CharacterController>().enabled = true;
        passenger = null;
        isEnter = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == transform.tag)
            return;
        if (!(collision.transform.tag == "Untagged" || collision.transform.tag == "Sensor" || collision.transform.tag == "Terrain" || collision.transform.tag == "Item" || collision.transform.tag == "Regdoll"))
        {
            collision.transform.GetComponent<ObjectCtrl>().GetDamage(CarRigidbody.velocity.magnitude * 1000f);
        }
        
    }
}
