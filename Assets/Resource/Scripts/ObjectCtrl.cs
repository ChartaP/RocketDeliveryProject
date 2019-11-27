using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eObjectType
{
    Unit = 0,
    Car = 1
}
public class ObjectCtrl : MonoBehaviour
{
    protected Vector3 Move_Dir = Vector3.zero;

    public float fSpeed =5;
    public float fJump  =10;
    protected const float fGracity = 9.8f;
    [SerializeField]
    protected float fMaxHP = 100f;

    protected float fCurHP = 100f;

    protected bool isDead = false;
    
    public float cameraHeight = 2;
    public float cameraDistance=2;

    [SerializeField]
    protected eObjectType eObjType = eObjectType.Unit;
    
    public eObjectType ObjType { get { return eObjType; } }

    protected virtual void Dead()
    {

    }
    public virtual int GetSpeed()
    {
        return 0;
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

    public virtual void GetDamage(float fDamage)
    {
        fCurHP = fCurHP - fDamage <= 0 ? 0 : fCurHP - fDamage;
    }

    public int MaxHP
    {
        get{return (int)fMaxHP;}
    }

    public int CurHP
    { get{ return (int)fCurHP;}
    }
}
