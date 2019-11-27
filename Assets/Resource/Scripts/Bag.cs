using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField]
    private List<Transform> GoodsList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LoadGoods(Transform Goods)
    {
        if (GoodsList.Count >= 3)
        {
            return false;
        }
        else
        {
            Goods.parent = transform;
            Goods.localPosition = new Vector3(0,GoodsList.Count*1.1f,0);
            Goods.localScale = new Vector3(1, 1, 1);
            Goods.rotation = transform.rotation;
            GoodsList.Add(Goods);
            return true;
        }
    }

    public bool UnloadGoods(int ID,out Transform Goods)
    {
        if(GoodsList.Count == 0)
        {
            Goods = null;
            return false;
        }
        else
        {
            for(int n = 0;n<GoodsList.Count; n++)
            {
                if (GoodsList[n].GetComponent<Goods>().ID == ID)
                {
                    GoodsList[n].parent = NPCGenerator.Instance.ObjectTrans;
                    Goods = GoodsList[n];
                    GoodsList.RemoveAt(n);
                    for(int m = n; m < GoodsList.Count; m++)
                    {
                        GoodsList[n].localPosition = new Vector3(0, m * 1.1f, 0);
                    }
                    return true;
                }
            }
            Goods = null;
            return false;
        }
    }

    public bool IsInBagGoods(int GoodsID)
    {
        foreach(Transform goods in GoodsList)
        {
            if (goods.GetComponent<Goods>().ID == GoodsID)
                return true;
        }
        return false;
    }
}
