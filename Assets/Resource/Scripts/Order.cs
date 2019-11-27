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

    public GameObject PausePrefab = null;

    private bool isPause = false;

    private Transform PauseObj = null;

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
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPause && PauseObj!=null)
            {
                isPause = false;
                Destroy(PauseObj.gameObject, 0.01f);
            }
            else
            {
                if (Time.timeScale == 1.0f)
                {
                    isPause = true;
                    PauseObj = Instantiate(PausePrefab, ReceiptTrans).transform;
                }
            }
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
        GameObject receipt =  Instantiate(ReceiptPrefab, ReceiptTrans);
        int reward = timeToReward(Timer);
        receipt.GetComponent<Receipt>().Set(GoodsName,FloatToStringTime(Timer),timeToScore(Timer), reward);
        PlayerCtrl.Instance.GetMoney(reward);
        nOrderStack--;
    }

    private int timeToScore(float time)
    {
        if (time < 30)
        {
            return 5;
        }
        else if(time< 60)
        {
            return 4;
        }
        else if (time < 120)
        {
            return 3;
        }
        else if (time < 240)
        {
            return 2;
        }
        else if (time < 300)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private int timeToReward(float time)
    {
        switch (timeToScore(time))
        {
            case 1:
                return 8350;
            case 2:
                return 9000;
            case 3:
                return 10000;
            case 4:
                return 13000;
            case 5:
                return 16700;
            default:
                return 0;
        }
    }

    private string FloatToStringTime(float fTime)
    {
        string m = ((int)(fTime / 60)).ToString();
        string s = ((int)(fTime % 60)).ToString();
        return m + "분 " + s+"초";
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
