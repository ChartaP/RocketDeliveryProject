using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    [SerializeField]
    private Transform LUpperArm;
    [SerializeField]
    private Transform RUowerArm;
    [SerializeField]
    private Transform chest;
    private Vector3 PointPos = Vector3.zero;
    
    public void Aimming(float Angle)
    {
        //chest.LookAt(Pos);
        //chest.eulerAngles += transform.right * Angle;
        //chest.Rotate(Vector3.right, Angle);
        chest.RotateAround(chest.position,transform.right, Angle);
    }

    public void GetViewPointPos(Vector3 Pos)
    {
        PointPos = Pos;
    }
}
