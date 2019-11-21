using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl instance = null;
    [SerializeField]
    private Transform CameraTarget = null;
    private Transform manTrans = null;
    public CameraFollow cameraFollow = null;
    public CameraInteraction cameraInteraction = null;
    public Transform CharTrans;
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
            CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
            manTrans = CharTrans;
            CameraTarget.parent = manTrans;
            CameraTarget.transform.localPosition = new Vector3(0,2,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!bTakeCar)
                cameraInteraction.Interact();
            else
                TakeOutCar();
        }

        if (bTakeCar)
        {
            CameraTarget.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 1f);
        }
        else
        {
            CharTrans.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 1f);
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
        ChangeCtrl(CarTrans);
    }

    public void TakeOutCar()
    {
        bTakeCar = false;
        ChangeCtrl(manTrans);
    }

    public void ChangeCtrl(Transform CharTrans)
    {
        this.CurCtrl.Exit();
        this.CharTrans = CharTrans;
        CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
        this.CurCtrl.Enter();
        CameraTarget.parent = CharTrans;
        CameraTarget.transform.localPosition = new Vector3(0,2,0);
        CameraTarget.transform.rotation =  Quaternion.Euler(0,0,0);
    }

    
}
