using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    private Transform curHit = null;
    private bool isEnable = true;

    public bool IsEnable
    {
        set
        {
            isEnable = value;
            if (!value)
            {
                if (curHit != null)
                {
                    Outline outLine;
                    if (curHit.tag == "Car")
                    {
                        outLine = curHit.GetComponent<Outline>();
                        outLine.OutlineWidth = 0.0f;
                    }
                    curHit = null;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    public void InteractUpdate()
    {
        if (!isEnable)
            return;
        RaycastHit hit;
        Outline outLine;
        LayerMask mask = (-1) - (1 << LayerMask.NameToLayer("Player"));

        if (Physics.Raycast(MainCamera.transform.position,MainCamera.transform.forward,out hit,100f, mask))
        {
            if (hit.transform != curHit && curHit != null)
            {
                if (curHit.tag == "Car")
                {
                    outLine = curHit.GetComponent<Outline>();
                    outLine.OutlineWidth = 0.0f;
                }
            }
            if(hit.transform.tag == "Car")
            {
                outLine = hit.transform.GetComponent<Outline>();
                outLine.OutlineWidth = 10.0f;
            }
            curHit = hit.transform;
        }
        else if(curHit != null)
        {
            if (curHit.tag == "Car")
            {
                outLine = curHit.GetComponent<Outline>();
                outLine.OutlineWidth = 0.0f;
            }
            curHit = null;
        }
    }


    public void Interact()
    {
        if (curHit != null)
        {
            if (curHit.tag == "Car")
            {
                PlayerCtrl.Instance.TakeInCar(curHit);

            }
        }
    }
}
