using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponmanCtrl : CharacterCtrl
{
    
    [SerializeField]
    private Transform bagTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        fCurHP = fMaxHP;
        Punch.RegOwner(this);
        CharCtrl = transform.GetComponent<CharacterController>();
        Move_Dir = Vector3.zero;
        Neck = AniCtrl.GetBoneTransform(HumanBodyBones.Neck);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurHP <= 0)
        {
            Dead();
        }
    }
    public override void Ctrl(float X, float Y, float Z)
    {
        if (isDead)
            return;
        if (CharCtrl.isGrounded)
        {
            AniCtrl.SetBool("isGround", true);
            Move_Dir = new Vector3(X, 0, Z);
            Move_Dir = transform.TransformDirection(Move_Dir);
            Move_Dir *= fSpeed;

            Move_Dir.y = Y * fJump;
            if(Y * fJump > 1f)
            {
                AniCtrl.SetTrigger("Jump");
            }
        }
        else
        {
            LayerMask mask = (-1) - (1 << LayerMask.NameToLayer("Player"));
            if (!Physics.Raycast(transform.position, -1*transform.up, 1f, mask))
            {
                AniCtrl.SetBool("isGround", false);
            }
        }

        Move_Dir.y -= fGracity * Time.deltaTime;
        AniCtrl.SetFloat("zSpeed", Z * fSpeed);
        CharCtrl.Move(Move_Dir * Time.deltaTime);
    }

    
}
