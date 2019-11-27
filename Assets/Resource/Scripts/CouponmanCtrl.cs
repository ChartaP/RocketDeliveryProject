﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponmanCtrl : CharacterCtrl
{
    
    public Bag myBag;
    
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

    protected override void Dead()
    {
        if (isDead)
            return;
        isDead = true;
        if (nWeaponState != 0)
        {
            weaponList[0].GetComponent<Weapon>().DropItem(transform);
        }
        Voice(3, true);
        NPCGenerator.Instance.UnregNPC(gameObject);
        Regdoll.transform.parent = transform.parent;
        Regdoll.SetActive(true);
        PlayerCtrl.Instance.manTrans = Regdoll.transform;
        Destroy(gameObject, 0.01f);
        Order.Instance.GameOver();
    }

    public override void GetItem(DropItem item)
    {
        AniCtrl.SetTrigger("Pick");
        eItem itemNum;
        GameObject prefab;
        item.Pick(out prefab, out itemNum);
        if ((int)itemNum / 10 == 2)
        {
            switch (itemNum)
            {
                case eItem.AKM:
                    if (weaponList[0] == null)
                    {
                        weaponList[0] = Instantiate(prefab, RHand).transform;
                        if (nWeaponState != 1)
                            weaponList[0].gameObject.SetActive(false);
                    }
                    ItemDropInspector.Instance.ItemDrop("Assault Rifle - AKM");
                    break;
            }
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
            if (Move_Dir.magnitude > 0.0f)
            {
                PlayStepSound();
            }
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

    public void GetChargeState(out string curCharge, out string maxCharge)
    {
        int cur=0;
        int max=0;
        if(nWeaponState == 0)
        {
            curCharge = "∞";
            maxCharge = "∞";
        }
        else
        {
            equipWeapon.GetComponent<Weapon>().GetChargeState(out cur,out max);
            curCharge = cur.ToString();
            maxCharge = max.ToString();
        }
    }
}
