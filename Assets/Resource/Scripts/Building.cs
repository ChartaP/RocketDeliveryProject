using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BuildingState
{
    IDLE = 0,
    FROM = 1,
    TO = 2
}

public class Building : MonoBehaviour
{
    public Sprite FromSprite = null;
    public Sprite ToSprite = null;
    public GameObject StateMark = null;
    public GameObject Effect = null;
    [SerializeField]
    private BuildingState myState = BuildingState.IDLE;
    [SerializeField]
    private bool isActive = false;

    private Color myColor = Color.white;

    private Transform Goods = null;

    private float fTimer = 0f;

    public bool IsActive { get { return isActive; } }

    // Start is called before the first frame update
    void Start()
    {
        Order.Instance.RegBuilding(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            fTimer += 1.0f * Time.deltaTime;
        }
    }
    
    public void SetFromPoint(Transform Goods, Color iconColor)
    {
        this.Goods = Goods;
        myState = BuildingState.FROM;
        Activation(iconColor);
    }

    public void SetToPoint(Transform Goods, Color iconColor)
    {
        this.Goods = Goods;
        myState = BuildingState.TO;
        Activation(iconColor);
    }

    public void Activation(Color iconColor)
    {
        isActive = true;
        fTimer = 0f;
        StateMark.SetActive(true);
        Effect.SetActive(true);
        switch (myState)
        {
            case BuildingState.FROM:
                StateMark.GetComponent<SpriteRenderer>().sprite = FromSprite;
                break;
            case BuildingState.TO:
                StateMark.GetComponent<SpriteRenderer>().sprite = ToSprite;
                break;
        }
        myColor = iconColor;
        StateMark.GetComponent<SpriteRenderer>().color = myColor;
    }

    public void Inactive()
    {
        isActive = false;
        myState = BuildingState.IDLE;
        StateMark.SetActive(false);
        Effect.SetActive(false);
        Goods = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            switch (myState)
            {
                case BuildingState.FROM:
                    SendGoods();
                    break;
                case BuildingState.TO:
                    RecvGoods();
                    break;
            }
        }
    }

    /// <summary>
    /// 배달부에게 배송품 전달
    /// </summary>
    private void SendGoods()
    {
        if (PlayerCtrl.Instance.manTrans.GetComponent<CouponmanCtrl>().myBag.LoadGoods(Goods))
        {
            ItemDropInspector.Instance.ItemDrop("배송지에서 화물 수령 완료");
            Inactive();
        }
    }

    /// <summary>
    /// 배달부에게 배송품 받음
    /// </summary>
    private void RecvGoods()
    {
        if(PlayerCtrl.Instance.manTrans.GetComponent<CouponmanCtrl>().myBag.IsInBagGoods(Goods.GetComponent<Goods>().ID))
        {
            Transform temp = null;
            PlayerCtrl.Instance.manTrans.GetComponent<CouponmanCtrl>().myBag.UnloadGoods(out temp);
            Inactive();
            Order.Instance.CompleteOrder(temp.GetComponent<Goods>().ID.ToString(),fTimer);
            Destroy(temp.gameObject, 0.1f);
            Order.Instance.ReturnIconColor(myColor);
        }
    }
}
