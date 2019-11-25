using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : ObjectCtrl
{
    [SerializeField]
    protected CharacterController CharCtrl = null;
    [SerializeField]
    protected Animator AniCtrl = null;
    [SerializeField]
    protected int nWeaponState = 0;
    [SerializeField]
    public GunAim gunAim = null;
    protected Transform Neck = null;
    [SerializeField]
    protected GameObject[] weaponList = new GameObject[1];
    protected GameObject equipWeapon = null;
    [SerializeField]
    protected Transform RHand;
    [SerializeField]
    protected Weapon Punch;
    [SerializeField]
    protected GameObject Regdoll = null;

    public int CurWeapon
    {
        get
        {
            return nWeaponState;
        }
    }

    public bool ChangeWeapon(int state)
    {
        
        if (weaponList.Length < state)
            return false;
        if (state != 0)
        {
            if (weaponList[state - 1] == null)
                return false;
        }
        
        TriggerWeapon(false);
        if (equipWeapon != null)
            Destroy(equipWeapon);
        nWeaponState = state;
        AniCtrl.SetInteger("nWeaponState", state);
        if (state != 0)
        {
            equipWeapon = Instantiate(weaponList[state - 1], RHand);
            equipWeapon.tag = transform.tag;
            equipWeapon.GetComponent<Weapon>().RegOwner(this);
        }
        else
            equipWeapon = null;
        
        return true;
    }

    public void TriggerWeapon(bool isTrigger)
    {
        if(nWeaponState == 0)
        {
            if (isTrigger)
                Punch.Trigger(AniCtrl);
            else
                Punch.Untrigger();
        }
        else
        {
            if (isTrigger)
                equipWeapon.GetComponent<Weapon>().Trigger(AniCtrl);
            else
                equipWeapon.GetComponent<Weapon>().Untrigger();
        }
    }

    public void ViewCtrl(float angle, Quaternion ViewTargetRotation, Vector3 viewPointPos)
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation , ViewTargetRotation, 24f*Time.deltaTime);
            
            Neck.LookAt(viewPointPos);
            gunAim.GetViewPointPos(viewPointPos);
            gunAim.Aimming(angle);
        }
    }

    public void GetItem(DropItem item)
    {
        AniCtrl.SetTrigger("Pick");
        eItem itemNum;
        GameObject prefab;
        item.Pick(out prefab,out itemNum);
        if ((int)itemNum / 10 == 2)
        {
            switch (itemNum)
            {
                case eItem.AKM:
                    weaponList[0] = prefab;
                    break;
            }
        }
    }

    public bool IsWeaponAim(GameObject target)
    {
        if (nWeaponState == 0)
        {
            return Punch.IsAim(target);
        }
        else
        {
            return equipWeapon.GetComponent<Weapon>().IsAim(target);
        }
    }

    public override void Enter()
    {
        AniCtrl.SetBool("isDrive", true);
    }

    public override void Exit()
    {
        AniCtrl.SetBool("isDrive", false);
    }
}
