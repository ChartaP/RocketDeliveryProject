using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float fTriggerDelay = 0f;
    [SerializeField]
    protected float fCooldown = 2f;
    [SerializeField]
    protected float fDamage = 20f;
    [SerializeField]
    protected float fDistance = 2.5f;
    [SerializeField]
    protected Transform startpoint;

    [SerializeField]
    protected IEnumerator work = null;

    [SerializeField]
    protected bool isTrigger = false;

    [SerializeField]
    protected CharacterCtrl owner;
    
    public virtual void RegOwner(CharacterCtrl owner)
    {
        this.owner = owner;
    }

    public virtual void Trigger(Animator Anim)
    {
        if (isTrigger)
            return;
        isTrigger = true;
        work = Work(Anim);
        StartCoroutine(work);
    }

    public virtual void Untrigger()
    {
        if (!isTrigger)
            return;
        isTrigger = false;
    }

    protected virtual IEnumerator Work(Animator Anim)
    {
        yield return new WaitForSeconds(fTriggerDelay);
        while (true)
        {
            if (!isTrigger)
                break;
            Fire(Anim);

            yield return new WaitForSeconds(fCooldown);
        }
        work = null;
        yield break;
    }

    protected virtual void Fire(Animator Anim)
    {
        Anim.SetTrigger("WeaponFire");
    }


}
