using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCarType
{
    Close = 0,
    Open = 1
}

public enum eSpeed {
    Idle = 0,
    Low = 1,
    Med = 2,
    High = 3,
    Max = 4
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
    private AudioClip[] EngineSounds;
    [SerializeField]
    private AudioSource CarAudio;


    [SerializeField]
    private List<Transform> WheelMesh = new List<Transform>();
    [SerializeField]
    private eCarType eType = eCarType.Close;
    [SerializeField]
    private eSpeed eCurEngineState = eSpeed.Idle;
    private float Speed;

    private bool isAccel = false;

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
        StartCoroutine("SpeedUpdate");
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

    IEnumerator EngineSound()
    {
        CarAudio.loop = false;
        CarAudio.clip = EngineSounds[0];
        CarAudio.Play();
        eSpeed SpeedState = eSpeed.Idle;
        bool bAccel = false;
        while(CarAudio.isPlaying)
        {
            yield return null;
        }
        CarAudio.loop = true;
        CarAudio.clip = EngineSounds[1];
        CarAudio.Play();
        while (isEnter)
        {
            if (Speed < 2)
            {
                SpeedState = eSpeed.Idle;
            }
            else if(Speed < 30)
            {
                SpeedState = eSpeed.Low;
            }
            else if (Speed < 60)
            {
                SpeedState = eSpeed.Med;
            }
            else
            {
                SpeedState = eSpeed.High;
            }
            if (eCurEngineState != SpeedState)
            {
                eCurEngineState = SpeedState;
                switch (eCurEngineState)
                {
                    case eSpeed.Idle:
                        CarAudio.clip = EngineSounds[1];
                        CarAudio.Play();
                        break;
                    case eSpeed.Low:
                        CarAudio.clip = EngineSounds[bAccel ? 3 : 2];
                        CarAudio.Play();
                        break;
                    case eSpeed.Med:
                        CarAudio.clip = EngineSounds[bAccel ? 5 : 4];
                        CarAudio.Play();
                        break;
                    case eSpeed.High:
                        CarAudio.clip = EngineSounds[bAccel ? 7 : 6];
                        CarAudio.Play();
                        break;
                }
            }
            if(isAccel != bAccel)
            {
                bAccel = isAccel;
                switch (eCurEngineState)
                {
                    case eSpeed.Idle:
                        CarAudio.clip = EngineSounds[1];
                        CarAudio.Play();
                        break;
                    case eSpeed.Low:
                        CarAudio.clip = EngineSounds[bAccel?3:2];
                        CarAudio.Play();
                        break;
                    case eSpeed.Med:
                        CarAudio.clip = EngineSounds[bAccel?5:4];
                        CarAudio.Play();
                        break;
                    case eSpeed.High:
                        CarAudio.clip = EngineSounds[bAccel?7:6];
                        CarAudio.Play();
                        break;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    public void Accel(float fSpeed)
    {
        isAccel = (fSpeed > 0.1f);
        if (curTrans == eTransmission.P && fSpeed > 0.1f)
        {
            BRightWheel.brakeTorque = 0;
            BLeftWheel.brakeTorque = 0;
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
        else if(fSpeed>0.1f)
        {
            curTrans = eTransmission.P;
            
        }
    }

    public void Steering(float fDir)
    {
        FRightWheel.steerAngle = fDir * 40;
        FLeftWheel.steerAngle = fDir * 40;
    }

    private IEnumerator SpeedUpdate()
    {
        Vector3 curPos = transform.position;
        curPos.y = 0;
        Vector3 prePos = curPos;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            curPos = transform.position;
            curPos.y = 0;
            Speed =Vector3.Distance(prePos, curPos) * 360*5  / 100 ;
            if (curTrans == eTransmission.P)
                Speed = 0;
            prePos = curPos;
        }
        yield break;
    }
    public int GetSpeed()
    {
        return (int)(Speed);
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
        StartCoroutine("EngineSound");
        FRightWheel.brakeTorque = 0;
        FLeftWheel.brakeTorque = 0;
        BRightWheel.brakeTorque = 0;
        BLeftWheel.brakeTorque = 0;
    }

    private void CarUnactivate()
    {
        StopCoroutine("EngineSound");
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
        passenger.transform.position = transform.position + (Vector3.right *- 2f)+ (Vector3.up * 2f);
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
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enemy" || collision.transform.tag == "Car")
        {
            collision.transform.GetComponent<ObjectCtrl>().GetDamage((Speed>20f?Speed:0) * 4 );
        }
        
    }
}
