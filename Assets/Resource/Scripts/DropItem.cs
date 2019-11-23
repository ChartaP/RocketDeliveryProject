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
    private eItem ItemNum = eItem.AKM;

    [SerializeField]
    private GameObject equipPrefab = null;


    private void Update()
    {
        model.transform.Rotate(Vector3.up, 60f * Time.deltaTime);
    }
}
