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
    protected GunAim gunAim = null;
    protected Transform Neck = null;
    [SerializeField]
    protected GameObject[] weaponList = new GameObject[1];

    [SerializeField]
    protected Transform RHand;

    public bool ChangeWeapon(int state)
    {
        if (weaponList.Length < state)
            return false;
        if (weaponList[state-1] == null)
            return false;
        nWeaponState = state;
        AniCtrl.SetInteger("nWeaponState", state);
        return true;
    }

    public void ViewCtrl(float angle, Quaternion ViewTargetRotation, Vector3 viewPointPos)
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, ViewTargetRotation, 10 * Time.deltaTime);
            Neck.LookAt(viewPointPos);
            //Neck.rotation = Neck.rotation * Quaternion.Euler(Neck.position - viewPointPos);
            gunAim.GetViewPointPos(viewPointPos);
            gunAim.Aimming(angle);
        }
    }
}
