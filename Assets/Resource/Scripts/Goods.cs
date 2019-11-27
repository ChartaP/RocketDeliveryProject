using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    private static int GoodsCnt = 0;
    [SerializeField]
    private int GoodsID;
    [SerializeField]
    private string goodsName;

    public int ID
    {
        get
        {
            return GoodsID;
        }
    }

    public string GoodsName
    {
        get
        {
            return goodsName;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        GoodsID = GoodsCnt++;
        goodsName = GenerateName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GenerateName()
    {
        switch (Random.Range(0, 10))
        {
            case 0:
                return "치킨";
            case 1:
                return "피자";
            case 2:
                return "짜장면";
            case 3:
                return "컴퓨터";
            case 4:
                return "TV";
            case 5:
                return "책";
            case 6:
                return "햄버거";
            case 7:
                return "아령";
            case 8:
                return "카메라";
            case 9:
                return "생수";
            default:
                return "Error";
        }

    }
}
