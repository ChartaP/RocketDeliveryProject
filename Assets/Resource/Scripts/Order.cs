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
    private GameObject GameoverPrefab;
    [SerializeField]
    private int nOrderStack = 0;

    public GameObject GoodsPrefab = null;

    [SerializeField]
    private List<Color> IconColorList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nOrderStack < 3)
        {
            OrderBuilding();
        }
    }

    public bool OrderBuilding()
    {
        Building From = null;
        Building To = null;
        GameObject Goods = null;
        Color IconColor = Color.white;
        if (BuildingList.Count-(nOrderStack*2) < 2)
            return false;
        Goods = IssueGoods();
        IconColor = GetIconColor();
        int randNum = Random.Range(0, BuildingList.Count);
        for (int n = 0; n <  BuildingList.Count; n++  )
        {
            int index = (n + randNum) % BuildingList.Count;
            if (BuildingList[index].IsActive)
                continue;
            else
            {
                BuildingList[index].SetFromPoint(Goods.transform, IconColor);
                From = BuildingList[index];
                break;
            }
        }
        if (From == null)
            return false;
        randNum = Random.Range(0, BuildingList.Count);
        for (int n = 0; n < BuildingList.Count; n++)
        {
            int index = (n + randNum) % BuildingList.Count;
            if (BuildingList[index].IsActive)
                continue;
            else
            {
                BuildingList[index].SetToPoint(Goods.transform, IconColor);
                To = BuildingList[index];
                break;
            }
        }
        if (To == null)
            return false;
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

    public void GameOver()
    {
        Instantiate(GameoverPrefab, ReceiptTrans);
    }

    public Color GetIconColor()
    {
        Color tmp = IconColorList[0];
        IconColorList.RemoveAt(0);
        return tmp;
    }

    public void ReturnIconColor(Color returnColor)
    {
        IconColorList.Add(returnColor);
    }
}
