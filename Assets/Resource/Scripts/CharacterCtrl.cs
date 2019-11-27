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
    protected Transform[] weaponList = new Transform[1];
    protected Transform equipWeapon = null;
    [SerializeField]
    protected Transform RHand;
    [SerializeField]
    protected Weapon Punch;
    [SerializeField]
    protected GameObject Regdoll = null;
    [SerializeField]
    protected AudioClip[] StepSound;
    [SerializeField]
    protected AudioSource CharAudio;


    public int CurWeapon {get{return nWeaponState;}}

    protected override void Dead()
    {
        if (isDead)
            return;
        isDead = true;
        if(nWeaponState != 0)
        {
            weaponList[0].GetComponent<Weapon>().DropItem(transform);
        }
        NPCGenerator.Instance.UnregNPC(gameObject);
        Regdoll.transform.parent = transform.parent;
        Regdoll.SetActive(true);
        Destroy(gameObject, 0.01f);
    }

    public bool IsWeaponStandBy()
    {
        if(nWeaponState == 0)
        {
            return true;
        }
        else
        {
            return equipWeapon.GetComponent<Weapon>().IsStandBy();
        }
    }

    public bool ChangeWeapon(int state)
    {
        if (state == nWeaponState)
            return false;
        if (weaponList.Length < state)
            return false;
        if (state != 0)
        {
            if (weaponList[state - 1] == null)
            {
                return false;
            }

        }
        
        TriggerWeapon(false);
        nWeaponState = state;
        AniCtrl.SetInteger("nWeaponState", state);
        if (state != 0)
        {
            equipWeapon = weaponList[state - 1];
            equipWeapon.gameObject.SetActive(true);
            equipWeapon.tag = transform.tag;
            equipWeapon.GetComponent<Weapon>().RegOwner(this);
        }
        else
        {
            equipWeapon.gameObject.SetActive(false);
            equipWeapon = null;
        }
        
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

    public void ReloadWeapon()
    {
        if(nWeaponState == 0)
        {

        }
        else
        {
            equipWeapon.GetComponent<Weapon>().Reload(AniCtrl);
        }
    }

    public void ViewCtrl(float angle, Quaternion ViewTargetRotation, Vector3 viewPointPos)
    {
        if (isDead)
            return;
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation , ViewTargetRotation, 24f * Time.deltaTime);
            
            Neck.LookAt(viewPointPos);
            gunAim.GetViewPointPos(viewPointPos);
            gunAim.Aimming(angle);
        }
    }

    public virtual void GetItem(DropItem item)
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
                    weaponList[0] = Instantiate( prefab,RHand).transform;
                    weaponList[0].gameObject.SetActive(false);
                    break;
            }
        }
    }

    public bool IsWeaponAim(GameObject target)
    {
        if (target == null)
            return false;
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
        ChangeWeapon(0);
        AniCtrl.SetBool("isDrive", true);
        CharCtrl.height = 1.0f;
    }

    public override void Exit()
    {
        AniCtrl.SetBool("isDrive", false);
        CharCtrl.height = 1.9f;
    }
}
