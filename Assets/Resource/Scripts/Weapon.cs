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
    protected GameObject bullet = null;

    [SerializeField]
    protected IEnumerator work = null;

    [SerializeField]
    protected bool isTrigger = false;

    [SerializeField]
    protected CharacterCtrl owner;

    [SerializeField]
    protected GameObject itemPrefab = null;
    [SerializeField]
    protected AudioClip TriggerSound;
    [SerializeField]
    protected AudioSource WeaponAudio = null;
    
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
            if (!Fire(Anim))
            {
                WeaponAudio.clip = TriggerSound;
                WeaponAudio.Play();
                break;
            }

            yield return new WaitForSeconds(fCooldown);
        }
        work = null;
        yield break;
    }

    public virtual bool Reload(Animator Anim)
    {
        return true;
    }

    public virtual bool IsStandBy()
    {
        return true;
    }

    protected virtual bool Fire(Animator Anim)
    {
        Anim.SetTrigger("WeaponFire");
        GameObject temp = Instantiate(bullet, startpoint.position, startpoint.rotation, owner.transform.parent);
        temp.tag = owner.tag;
        temp.GetComponent<Bullet>().SetBullet(fDamage, 1);
        temp.GetComponent<Rigidbody>().AddForce(startpoint.forward * 500f);
        return true;
    }

    public virtual bool IsAim(GameObject target)
    {
        RaycastHit[] hits;
        hits = Physics.BoxCastAll(startpoint.position, new Vector3(0.4f, 0.4f, 0.4f), startpoint.forward, startpoint.rotation, fDistance);
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.tag == target.tag)
            {
                return true;
            }
        }
        return false;
    }
    public void DropItem(Transform pos)
    {
        if(itemPrefab != null)
        {
            Instantiate(itemPrefab, pos.position, pos.rotation, NPCGenerator.Instance.ObjectTrans);
        }
    }
}
