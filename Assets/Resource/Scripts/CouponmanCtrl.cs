using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponmanCtrl : CharacterCtrl
{
    private CharacterController CharCtrl = null;
    // Start is called before the first frame update
    void Start()
    {
        CharCtrl = transform.GetComponent<CharacterController>();
        Move_Dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Ctrl(float X, float Y, float Z)
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, PlayerCtrl.Instance.CameraTarget.rotation, 10 * Time.deltaTime);
        }
        if (CharCtrl.isGrounded)
        {
            Move_Dir = new Vector3(X, 0, Z);
            Move_Dir = transform.TransformDirection(Move_Dir);
            Move_Dir *= fSpeed;

            Move_Dir.y = Y * fJump;
        }

        Move_Dir.y -= fGracity * Time.deltaTime;

        CharCtrl.Move(Move_Dir * Time.deltaTime);
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }
}
