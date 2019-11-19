using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : CharacterCtrl
{
    private Rigidbody CarRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        CarRigidbody = transform.GetComponent<Rigidbody>();
        Move_Dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Ctrl(float X, float Y, float Z)
    {
        Move_Dir = new Vector3(X, 0, Z);
        Move_Dir = transform.TransformDirection(Move_Dir);
        Move_Dir *= fSpeed;

        Move_Dir.y = Y * fJump;

        Move_Dir.y -= fGracity * Time.deltaTime;


        //CharCtrl.Move(Move_Dir * Time.deltaTime);
    }
}
