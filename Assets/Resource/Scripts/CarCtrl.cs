using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CarCtrl : ObjectCtrl
{
    protected Car Car;
    [SerializeField]
    protected GameObject DestroyEffect = null;
    [SerializeField]
    protected float fEffectSize = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        fCurHP = fMaxHP;
        eObjType = eObjectType.Car;
        if(Car == null)
        {
            Car = transform.GetComponent<Car>();
        }
        Move_Dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurHP <= 0)
        {
            Dead();
        }
    }

    public override int GetSpeed()
    {
        return Car.GetSpeed();
    }

    protected override void Dead()
    {
        isDead = true;
        Instantiate(DestroyEffect, transform.position,transform.rotation).transform.localScale = new Vector3(fEffectSize, fEffectSize, fEffectSize);

        this.Car.CarExit();
        Destroy(gameObject, 0.01f);
    }

    public override void GetDamage(float fDamage)
    {
        float result = fDamage / 4;
        fCurHP = fCurHP - result <= 0 ? 0 : fCurHP - result;
    }

    public override void Ctrl(float X, float Y, float Z)
    {
        Car.Accel(Z * fSpeed * Time.deltaTime);

        Car.Break(Y * fSpeed );

        Car.Steering(X );

        //CharCtrl.Move(Move_Dir * Time.deltaTime);
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    
}
