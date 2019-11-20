using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl instance = null;
    public CameraFollow cameraFollow = null;
    public CameraInteraction cameraInteraction = null;
    public Transform CharTrans;
    private CharacterCtrl CurCtrl;

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
            cameraFollow.TargetChange(CharTrans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            cameraInteraction.Interact();
        }
    }

    public void ChangeCtrl(Transform CharTrans)
    {
        this.CurCtrl.Exit();
        this.CharTrans = CharTrans;
        CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
        this.CurCtrl.Enter();
        cameraFollow.TargetChange(CharTrans);

    }

    
}
