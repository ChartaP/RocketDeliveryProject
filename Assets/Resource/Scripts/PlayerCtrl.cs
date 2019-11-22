using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl instance = null;
    public Transform CameraTarget = null;
    public CameraFollow cameraFollow = null;
    public CameraInteraction cameraInteraction = null;
    public Transform manTrans;
    public Transform CurTrans;
    [SerializeField]
    private CharacterCtrl CurCtrl;
    [SerializeField]
    private bool bTakeCar = false;

    public static PlayerCtrl Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(PlayerCtrl)) as PlayerCtrl;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(CurCtrl == null)
        {
            CurCtrl = manTrans.GetComponent<CharacterCtrl>();
            CurTrans = manTrans;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        CameraTarget.position = CurCtrl.transform.position;
        CameraTarget.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!bTakeCar)
                cameraInteraction.Interact();
            else
                TakeOutCar();
        }
    }

    private void LateUpdate()
    {
        if(!bTakeCar)
            cameraInteraction.InteractUpdate();
    }

    public void TakeInCar(Transform CarTrans)
    {
        bTakeCar = true;
        cameraInteraction.IsEnable = false;
        CarTrans.GetComponent<Car>().CarEnter(CurCtrl.transform);
        ChangeCtrl(CarTrans);
    }

    public void TakeOutCar()
    {
        bTakeCar = false;
        cameraInteraction.IsEnable = true;
        CurCtrl.transform.GetComponent<Car>().CarExit();
        ChangeCtrl(manTrans);
    }

    public void ChangeCtrl(Transform CharTrans)
    {
        this.CurTrans = CharTrans;
        CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
        cameraFollow.SetPosition(CurCtrl.cameraHeight, CurCtrl.cameraDistance);
    }

    
}
