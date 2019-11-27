using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    private static int GoodsCnt = 0;
    [SerializeField]
    private int GoodsID;

    public int ID
    {
        get
        {
            return GoodsID;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        GoodsID = GoodsCnt++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
