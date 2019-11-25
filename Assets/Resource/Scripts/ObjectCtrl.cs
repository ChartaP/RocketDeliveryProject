using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCtrl : MonoBehaviour
{
    protected Vector3 Move_Dir = Vector3.zero;

    public float fSpeed =5;
    public float fJump  =10;
    protected const float fGracity = 9.8f;
    [SerializeField]
    protected float fMaxHP = 100f;

    protected float fCurHP = 100f;
    
    public float cameraHeight = 2;
    public float cameraDistance=2;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(fCurHP <= 0)
        {
            Dead();
        }
    }

    protected void Dead()
    {

    }

    public virtual void Ctrl(float X,float Y,float Z)
    {
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public void GetDamage(float fDamage)
    {
        fCurHP = fCurHP - fDamage <= 0 ? 0 : fCurHP - fDamage;
    }

    public float MaxHP
    {
        get{return fMaxHP;}
    }

    public float CurHP
    { get{ return fCurHP;}
    }
}
