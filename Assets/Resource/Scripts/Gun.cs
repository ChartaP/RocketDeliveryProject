﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField]
    private float fRecoil = 1.0f;

    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private GameObject effect = null;
    [SerializeField]
    private float fDrag = 0f;
    // Start is called before the first frame update

    protected override void Fire(Animator Anim)
    {
        Anim.SetTrigger("WeaponFire");
        Instantiate(effect, startpoint.position, startpoint.rotation);
        GameObject temp = Instantiate(bullet, startpoint.position, startpoint.rotation, owner.transform.parent);
        temp.tag = transform.tag;
        temp.GetComponent<bullet>().SetBullet(fDamage, fDrag);
        temp.GetComponent<Rigidbody>().AddForce(startpoint.forward*10000f);
        owner.gunAim.GetRecoil(fRecoil);
    }
}
