using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    public Camera MainCamera;
    [SerializeField]
    private Transform curHit = null;
    [SerializeField]
    private float distance = 5.0f;
    private bool isEnable = true;
    private Vector3 viewPointPos = Vector3.zero;

    public Vector3 ViewPointPos
    {
        get
        {
            return viewPointPos;
        }
    }
    
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        
        if (curHit != null)
        {
            if (isInteractable(curHit.tag))
            {
                GUI.Label(new Rect(Screen.width/2-64, (int)(Screen.height/1.5), 128, 64),"Press F", style);
            }
        }
    }

    // Update is called once per frame
    public void InteractUpdate()
    {
        if (!isEnable)
            return;
        RaycastHit hit;
        LayerMask mask = (-1) - (1 << LayerMask.NameToLayer("Player"));

        if (Physics.Raycast(MainCamera.transform.position,MainCamera.transform.forward,out hit,Mathf.Infinity, mask))
        {
            Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * distance, Color.green,0.01f);
            viewPointPos = hit.point;
            if (curHit != null)
            {
                if (hit.transform != curHit)
                {
                    if (isInteractable(curHit.tag))
                    {
                        outLineWidth(curHit, 0.0f);
                    }
                }
                else if (Vector3.Distance(transform.position, viewPointPos) > distance)
                {
                    if (isInteractable(curHit.tag))
                    {
                        outLineWidth(curHit, 0.0f);
                    }
                }
            }
            if (isInteractable(hit.transform.tag))
            {
                if (Vector3.Distance(transform.position, viewPointPos) < distance) {
                    outLineWidth(hit.transform, 10.0f);
                }
            }
            curHit = hit.transform;
        }
        else if(curHit != null)
        {
            if (isInteractable(curHit.tag))
            {
                outLineWidth(curHit, 0.0f);
            }
            curHit = null;
        }
    }

    private void outLineWidth(Transform target,float width)
    {
        Outline outLine;
        outLine = target.GetComponent<Outline>();
        outLine.OutlineWidth = width;
    }

    private bool isInteractable(string tag)
    {
        return tag == "Car" || tag == "Item";
    }

    public void Interact()
    {
        if (curHit != null)
        {
            switch (curHit.tag)
            {
                case "Car":
                    PlayerCtrl.Instance.TakeInCar(curHit);
                    break;
                case "Item":
                    PlayerCtrl.Instance.GetItem(curHit);
                    break;
                default:
                    break;
            }
        }
    }
}
