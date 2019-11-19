using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform CharTrans;
    private CharacterCtrl CurCtrl;

    // Start is called before the first frame update
    void Start()
    {
        if(CurCtrl == null)
        {
            CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurCtrl.Ctrl(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"), Input.GetAxis("Vertical"));

    }

    void ChangeCtrl(Transform CharTrans)
    {
        this.CharTrans = CharTrans;
        CurCtrl = CharTrans.GetComponent<CharacterCtrl>();
    }
}
