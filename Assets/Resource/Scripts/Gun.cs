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
    private AudioClip ReloadSound;

    [SerializeField]
    private float fDrag = 0f;

    [SerializeField]
    private int nCharge = 30;

    [SerializeField]
    private int curCharge = 0;

    [SerializeField]
    private float ReloadTime = 2f;
    

    // Start is called before the first frame update

    protected override bool Fire(Animator Anim)
    {
        if (curCharge > 0)
        {
            Anim.SetTrigger("WeaponFire");
            curCharge--;
            Instantiate(effect, startpoint.position, startpoint.rotation);
            GameObject temp = Instantiate(bullet, startpoint.position, startpoint.rotation, owner.transform.parent);
            temp.tag = transform.tag;
            temp.GetComponent<Bullet>().SetBullet(fDamage, fDrag);
            temp.GetComponent<Rigidbody>().AddForce(startpoint.forward * 5000f);
            owner.gunAim.GetRecoil(fRecoil);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Reload(Animator Anim)
    {
        StartCoroutine("Reloading",Anim);
        return true;
    }
    public override bool IsStandBy()
    {
        if(!(curCharge > 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected IEnumerator Reloading(Animator Anim)
    {
        Anim.SetTrigger("Reload");
        WeaponAudio.clip = ReloadSound;
        WeaponAudio.Play();
        yield return new WaitForSeconds(ReloadTime);
        curCharge = nCharge;
        yield break;
    }
    
}
