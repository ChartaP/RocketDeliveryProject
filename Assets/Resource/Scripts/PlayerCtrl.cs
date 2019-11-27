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
    [SerializeField]
    private int nMoney = 0;

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
        if (CurTrans != null)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 24;
            style.normal.textColor = Color.white;

            if (bTakeCar)
            {
                GUI.Label(new Rect(32, Screen.height - 64, 128, 32), CurCtrl.GetSpeed() + "Km/h", style);
            }
            GUI.Label(new Rect(32, Screen.height - 32, 128, 32), "HP:" + manTrans.GetComponent<ObjectCtrl>().CurHP + "/" + manTrans.GetComponent<ObjectCtrl>().MaxHP, style);
            string cur;
            string max;
            manTrans.GetComponent<CouponmanCtrl>().GetChargeState(out cur, out max);
            style.alignment = TextAnchor.MiddleRight;
            string weapon = "";
            switch (manTrans.GetComponent<CharacterCtrl>().CurWeapon)
            {
                case 0:
                    weapon = "불주먹";
                    break;
                case 1:
                    weapon = "AKM";
                    break;
            }
            GUI.Label(new Rect(Screen.width - 96, Screen.height - 32, 64, 32),weapon+"     "+ cur + "/" + max, style);
            GUI.Label(new Rect(Screen.width - 96, Screen.height - 64, 64, 32), nMoney + "원", style);
        }

    }

    // Start is called before the first frame update
    void Awake()
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetMoney(1000000);
        }
        if(CurCtrl == null )
        {
            if (bTakeCar)
            {
                bTakeCar = false;
                cameraInteraction.IsEnable = true;
                ChangeCtrl(manTrans);
            }
            else
                return;
        }
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        
        CameraTarget.position = CurCtrl.transform.position;
        CameraTarget.Rotate(Vector3.up * Input.GetAxis("Mouse X") *Time.deltaTime * 60f);
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
            if (Input.GetKeyDown(KeyCode.R))
                (CurCtrl as CharacterCtrl).ReloadWeapon();
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

    public void GetMoney(int nReward)
    {
        nMoney += nReward;
    }

    public bool UseMoney(int nPrice)
    {
        if(nMoney - nPrice >= 0)
        {
            nMoney -= nPrice;
            return true;
        }
        else
        {
            return false;
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

    public void Purchase(Transform VendingTrans)
    {
        VendingMachine machine = VendingTrans.GetComponent<VendingMachine>();
        if (UseMoney(machine.Price))
        {
            machine.Purchase(CurCtrl);
        }
        else
        {
            MsgInspector.Instance.Msg("돈이 부족합니다");
        }
    }
}
