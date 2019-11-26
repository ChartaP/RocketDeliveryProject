using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private static Order instance = null;

    public static Order Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Order)) as Order;
            }
            return instance;
        }
    }

    [SerializeField]
    private List<Building> BuildingList = new List<Building>();

    [SerializeField]
    private Transform ReceiptTrans;
    [SerializeField]
    private GameObject ReceiptPrefab;

    [SerializeField]
    private int nOrderStack = 0;

    public GameObject GoodsPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nOrderStack < 1)
        {
            OrderBuilding();
        }
    }

    public bool OrderBuilding()
    {
        Building From = null;
        Building To = null;
        GameObject Goods = null;
        if (BuildingList.Count-(nOrderStack*2) < 2)
            return false;
        Goods = IssueGoods();
        int randNum = Random.Range(0, BuildingList.Count);
        for (int n = 0; n <  BuildingList.Count; n++  )
        {
            int index = (n + randNum) % BuildingList.Count;
            if (BuildingList[index].IsActive)
                continue;
            else
            {
                BuildingList[index].SetFromPoint(Goods.transform);
                From = BuildingList[index];
                break;
            }
        }
        randNum = Random.Range(0, BuildingList.Count);
        for (int n = 0; n < BuildingList.Count; n++)
        {
            int index = (n + randNum) % BuildingList.Count;
            if (BuildingList[index].IsActive)
                continue;
            else
            {
                BuildingList[index].SetToPoint(Goods.transform);
                To = BuildingList[index];
                break;
            }
        }
        if (From != null && To != null)
        {
            nOrderStack++;
            return true;
        }
        else
            return false;
    }

    public void RegBuilding(Building building)
    {
        BuildingList.Add(building);
    }

    public GameObject IssueGoods()
    {
        return Instantiate(GoodsPrefab);
    }

    public void CompleteOrder(string GoodsName,float Timer)
    {
        Instantiate(ReceiptPrefab, ReceiptTrans);
        nOrderStack--;
    }


}
