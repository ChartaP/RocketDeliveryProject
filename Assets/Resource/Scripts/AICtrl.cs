using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    public Sensor mySensor = null;

    public Transform manTrans;
    public Transform CurTrans;
    [SerializeField]
    private ObjectCtrl CurCtrl;
    [SerializeField]
    private bool bTakeCar = false;

    [SerializeField]
    private float fHorizontal = 1.0f;
    [SerializeField]
    private float fVertical = 1.0f;
    [SerializeField]
    private float fJump = 10.0f;

    private float curHorizontal = 0.0f;
    private float curVertical = 0.0f;
    private float curJump = 0.0f;

    [SerializeField]
    private float AtkRange = 10f;

    void Start()
    {
        if (CurCtrl == null)
        {
            CurCtrl = manTrans.GetComponent<ObjectCtrl>();
            CurTrans = manTrans;
        }
    }

    void Update()
    {
        GameObject target = null;
        if(mySensor.FindedUnit(out target))
        {
            if(Vector3.Distance(transform.position,target.transform.position) > AtkRange)
            {
                if ((CurCtrl as CharacterCtrl).CurWeapon != 0)
                    (CurCtrl as CharacterCtrl).ChangeWeapon(0);
                curVertical = fVertical;
            }
            else
            {
                if ((CurCtrl as CharacterCtrl).CurWeapon != 1)
                    (CurCtrl as CharacterCtrl).ChangeWeapon(1);
                if((CurCtrl as CharacterCtrl).IsWeaponAim(target))
                    (CurCtrl as CharacterCtrl).TriggerWeapon(true);
                else
                    (CurCtrl as CharacterCtrl).TriggerWeapon(false);
            }
        }
        else
        {
            if ((CurCtrl as CharacterCtrl).CurWeapon != 0)
                (CurCtrl as CharacterCtrl).ChangeWeapon(0);
        }
        bool rightWay = true;
        /*
        if(mySensor.IsDeadEnd(out rightWay))
        {
            if (rightWay)
                curHorizontal = fHorizontal;
            else
                curHorizontal = -fHorizontal;
        }*/
        CurCtrl.Ctrl(curHorizontal, curJump, curVertical);
        curHorizontal = Mathf.Lerp(curHorizontal, 0f, 4f * Time.deltaTime);
        curVertical = Mathf.Lerp(curVertical, 0f, 4f * Time.deltaTime);
        curJump = Mathf.Lerp(curJump, 0f, fJump * Time.deltaTime);

        //CameraTarget.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
        if (bTakeCar)
        {
            
        }
        else
        {
        }

    }

    private void LateUpdate()
    {
        FindEnemy();
        if (!bTakeCar)
        {
            //cameraInteraction.InteractUpdate();
            (CurCtrl as CharacterCtrl).ViewCtrl(mySensor.EyeDirX().eulerAngles.x+1f, mySensor.EyeDirY() ,mySensor.ViewPointPos);
        }
    }

    private void FindEnemy()
    {

    }

    public void TakeInCar(Transform CarTrans)
    {
        bTakeCar = true;
        //cameraInteraction.IsEnable = false;
        CarTrans.GetComponent<Car>().CarEnter(CurCtrl.transform);
        ChangeCtrl(CarTrans);
    }

    public void TakeOutCar()
    {
        bTakeCar = false;
        //cameraInteraction.IsEnable = true;
        CurCtrl.transform.GetComponent<Car>().CarExit();
        ChangeCtrl(manTrans);
    }

    public void ChangeCtrl(Transform CharTrans)
    {
        this.CurTrans = CharTrans;
        CurCtrl = CharTrans.GetComponent<ObjectCtrl>();
        //cameraFollow.SetPosition(CurCtrl.cameraHeight, CurCtrl.cameraDistance);
    }

    public void GetItem(Transform ItemTrans)
    {
        DropItem item = ItemTrans.GetComponent<DropItem>();
        (CurCtrl as CharacterCtrl).GetItem(item);
    }
}
