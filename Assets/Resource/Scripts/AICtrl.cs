using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    public Transform EyeTarget = null;

    public Transform manTrans;
    public Transform CurTrans;
    [SerializeField]
    private ObjectCtrl CurCtrl;
    [SerializeField]
    private bool bTakeCar = false;

    [SerializeField]
    private float fHorizontal = 8.0f;
    [SerializeField]
    private float fVertical = 8.0f;
    [SerializeField]
    private float fJump = 1000.0f;

    private float curHorizontal = 0.0f;
    private float curVertical = 0.0f;
    private float curJump = 0.0f;

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
            //(CurCtrl as CouponmanCtrl).ViewCtrl(0, transform.rotation , cameraInteraction.ViewPointPos);
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
