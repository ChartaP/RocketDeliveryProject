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

    private float fRecoil = 0.0f;

    public void GetRecoil(float power)
    {
        fRecoil += power;
    }

    private void Update()
    {
        if (fRecoil > 0)
        {
            fRecoil = Mathf.Lerp(fRecoil, 0f, 4f * Time.deltaTime);
        }
    }

    public void Aimming(float Angle)
    {
        //chest.LookAt(Pos);
        //chest.eulerAngles += transform.right * Angle;
        //chest.Rotate(Vector3.right, Angle);
        chest.RotateAround(chest.position,transform.right, Angle-fRecoil);
    }



    public void GetViewPointPos(Vector3 Pos)
    {
        PointPos = Pos;
    }
}
