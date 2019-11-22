using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl instance = null;
    public Transform CameraTarget = null;
    public Transform manTrans = null;
    public CameraFollow cameraFollow = null;
    public CameraInteraction cameraInteraction = null;
    public Transform CharTrans;
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
            CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
            manTrans = CharTrans;
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
    }

    
}
