using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : CharacterCtrl
{
    private Car Car;
    // Start is called before the first frame update
    void Start()
    {
        if(Car == null)
        {
            Car = transform.GetComponent<Car>();
        }
        Move_Dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Ctrl(float X, float Y, float Z)
    {
        Debug.Log( X +","+ Y+","+  Z);
        Car.Accel(Z * fSpeed);

        Car.Break(Y * fSpeed);

        Car.Handle(X * fSpeed);

        //CharCtrl.Move(Move_Dir * Time.deltaTime);
    }

    public override void Enter()
    {
        Car.CarActivate();
    }

    public override void Exit()
    {
        Car.CarUnactivate();
    }
}
