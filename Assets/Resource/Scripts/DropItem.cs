using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItem
{
    //1X - Item

    //2X - Weapon
    AKM = 20,
}
public class DropItem : MonoBehaviour
{
    [SerializeField]
    private GameObject model;

    [SerializeField]
    private eItem eItemNum = eItem.AKM;

    [SerializeField]
    private GameObject equipPrefab = null;


    private void Update()
    {
        model.transform.Rotate(Vector3.up, 60f * Time.deltaTime);
    }

    public GameObject GetPrefab()
    {
        return equipPrefab;
    }

    public eItem GetNum()
    {
        return eItemNum;
    }

    public bool Pick(out GameObject prefab, out eItem num)
    {
        prefab = equipPrefab;
        num = eItemNum;
        Destroy(transform.gameObject, 0.01f);
        return true;
    }
}
