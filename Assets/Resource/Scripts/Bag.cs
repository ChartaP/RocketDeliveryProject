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

    public bool UnloadGoods(out Transform Goods)
    {
        if(GoodsList.Count == 0)
        {
            Goods = null;
            return false;
        }
        else
        {
            GoodsList[GoodsList.Count - 1].parent = NPCGenerator.Instance.ObjectTrans;
            Goods = GoodsList[GoodsList.Count-1];
            GoodsList.RemoveAt(GoodsList.Count - 1);
            return true;

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
