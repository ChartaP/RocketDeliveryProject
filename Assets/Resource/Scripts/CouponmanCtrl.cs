using System.Collections;
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
        isDead = true;
        if (nWeaponState != 0)
        {
            weaponList[0].GetComponent<Weapon>().DropItem(transform);
        }
        NPCGenerator.Instance.UnregNPC(gameObject);
        Regdoll.transform.parent = transform.parent;
        Regdoll.SetActive(true);
        PlayerCtrl.Instance.manTrans = Regdoll.transform;
        Destroy(gameObject, 0.01f);
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
                    weaponList[0] = Instantiate( prefab,RHand).transform;
                    weaponList[0].gameObject.SetActive(false);
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
