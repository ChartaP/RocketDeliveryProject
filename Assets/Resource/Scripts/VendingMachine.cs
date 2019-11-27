using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField]
    private int nPrice = 6000;

    [SerializeField]
    private int nRegain = 50;

    public int Price
    {
        get
        {
            return nPrice;
        }
    }

    public int Regain
    {
        get
        {
            return nRegain;
        }
    }

    public void Purchase(ObjectCtrl obj)
    {
        obj.RegainHP(Regain);
    }
}
