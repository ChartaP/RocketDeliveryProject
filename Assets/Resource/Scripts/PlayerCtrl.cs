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
    private ObjectCtrl CurCtrl;
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

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(32,Screen.height -32, 128, 32), "HP:"+manTrans.GetComponent<ObjectCtrl>().CurHP+"/"+manTrans.GetComponent<ObjectCtrl>().MaxHP, style);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(CurCtrl == null)
        {
            CurCtrl = manTrans.GetComponent<ObjectCtrl>();
            CurTrans = manTrans;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        
        CameraTarget.position = CurCtrl.transform.position;
        CameraTarget.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
        if (bTakeCar)
        {
            if (Input.GetKeyDown(KeyCode.F))
                TakeOutCar();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
                cameraInteraction.Interact();
            if (Input.GetKeyDown(KeyCode.X))
                (CurCtrl as CharacterCtrl).ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha1))
                (CurCtrl as CharacterCtrl).ChangeWeapon(1);
            if (Input.GetMouseButtonDown(0))
                (CurCtrl as CharacterCtrl).TriggerWeapon(true);
            if (Input.GetMouseButtonUp(0))
                (CurCtrl as CharacterCtrl).TriggerWeapon(false);
        }
        
    }

    private void LateUpdate()
    {
        if (!bTakeCar)
        {
            cameraInteraction.InteractUpdate();
            (CurCtrl as CharacterCtrl).ViewCtrl(cameraFollow.Y, CameraTarget.rotation, cameraInteraction.ViewPointPos);
        }
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
        CurCtrl = CharTrans.GetComponent<ObjectCtrl>();
        cameraFollow.SetPosition(CurCtrl.cameraHeight, CurCtrl.cameraDistance);
    }

    public void GetItem(Transform ItemTrans)
    {
        DropItem item = ItemTrans.GetComponent<DropItem>();
        (CurCtrl as CharacterCtrl).GetItem(item);
    }
}
