using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField]
    private float fRecoil = 1.0f;
    
    [SerializeField]
    private GameObject effect = null;
    [SerializeField]
    private float fDrag = 0f;

    [SerializeField]
    private int nCharge = 30;

    [SerializeField]
    private int curCharge = 0;
    

    // Start is called before the first frame update

    protected override void Fire(Animator Anim)
    {
        Anim.SetTrigger("WeaponFire");
        weaponAudio.clip = triggerSound;
        weaponAudio.Play();
        Instantiate(effect, startpoint.position, startpoint.rotation);
        GameObject temp = Instantiate(bullet, startpoint.position, startpoint.rotation, owner.transform.parent);
        temp.tag = transform.tag;
        temp.GetComponent<Bullet>().SetBullet(fDamage, fDrag);
        temp.GetComponent<Rigidbody>().AddForce(startpoint.forward*5000f);
        owner.gunAim.GetRecoil(fRecoil);
    }
    
}
