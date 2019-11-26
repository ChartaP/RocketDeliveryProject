﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropInspector : MonoBehaviour
{
    private static ItemDropInspector instance = null;
    
    public static ItemDropInspector Instance{
        get{
            if (instance == null){
                instance = FindObjectOfType(typeof(ItemDropInspector)) as ItemDropInspector;
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject ItemDropPrefab = null;

    [SerializeField]
    private List<GameObject> TextList = new List<GameObject>();

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //    ItemDrop("Test Test ");
    }

    public void ItemDrop(string sText)
    {
        GameObject gText = Instantiate(ItemDropPrefab, transform);
        gText.GetComponent<Text>().text = sText;
        gText.transform.localPosition = new Vector3(32,-32 - (16 * TextList.Count),0);
        StartCoroutine("TextVisual", gText);
        TextList.Add(gText);
    }

    IEnumerator TextVisual(GameObject gText)
    {
        float fLifeTime = 2.0f;
        StartCoroutine("TextAppear", gText);
        yield return new WaitForSeconds(fLifeTime);
        TextList.RemoveAt(0);
        for(int n =0;n<TextList.Count; n++)
        {
            TextList[n].transform.localPosition = new Vector3(32, -32 - (16 * n), 0);
        }
        StartCoroutine("TextDisappear", gText);
        yield break;
    }

    IEnumerator TextAppear(GameObject gText)
    {
        Vector3 Pos = gText.transform.localPosition;
        Pos.y -= 16;
        gText.transform.localPosition = Pos;
        for (int n = 0; n < 16; n++)
        {
            Pos = gText.transform.localPosition;
            Pos.y += 1;
            gText.transform.localPosition = Pos;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    IEnumerator TextDisappear(GameObject gText)
    {
        Vector3 Pos;
        for (int n = 0; n < 16; n++)
        {
            Pos = gText.transform.localPosition;
            Pos.y+= 1;
            gText.transform.localPosition = Pos;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gText);
        yield break;
    }
}
