using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField]
    private Transform EyeTrans = null;
    [SerializeField]
    private Transform RightSensor = null;
    [SerializeField]
    private Transform LeftSensor = null;
    [SerializeField]
    private float fDistance = 50f;
    [SerializeField]
    private List<GameObject> FindUnit = new List<GameObject>();
    [SerializeField]
    private List<GameObject> FindItem = new List<GameObject>();

    private Vector3 viewPointPos = Vector3.zero;
    [SerializeField]
    private Transform curTrans = null;

    private float fRightDistance = 0f;
    private float fLeftDiatance = 0f;
    
    public Vector3 ViewPointPos
    {
        get
        {
            return viewPointPos;
        }
    }
    private void Start()
    {
        transform.GetComponent<SphereCollider>().radius = fDistance;
    }
    private void Update()
    {
        FocusEye();
        ViewPointUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindUnit.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindUnit.Remove(other.gameObject);
        }
    }

    private void FocusEye()
    {
        if (FindUnit.Count > 0)
        {
            Vector3 StartPos = transform.position;
            StartPos.y = 0;
            Vector3 EndPos = FindUnit[0].transform.position;
            EndPos.y = 0;
            transform.rotation = Quaternion.LookRotation(EndPos - StartPos, Vector3.up);

            EyeTrans.LookAt(FindUnit[0].transform.position + new Vector3(0f,1.6f,0f), Vector3.up);
            Debug.DrawRay(EyeTrans.position, EyeTrans.forward * fDistance, Color.green, 0.01f);
            Debug.DrawRay(RightSensor.position, RightSensor.forward * fDistance, Color.green, 0.01f);
            Debug.DrawRay(LeftSensor.position, LeftSensor.forward * fDistance, Color.green, 0.01f);
        }
    }

    public void ViewPointUpdate()
    {
        RaycastHit hit;
        LayerMask mask = (-1) - (1 << LayerMask.NameToLayer("Default"));

        if (Physics.Raycast(EyeTrans.position, EyeTrans.forward, out hit, Mathf.Infinity , mask))
        {
            viewPointPos = hit.point;
            if (hit.transform.tag == "Player")
                curTrans = hit.transform;
            else
                curTrans = null;
        }
        if (Physics.Raycast(RightSensor.position, RightSensor.forward, out hit, Mathf.Infinity, mask))
        {
            fRightDistance = Vector3.Distance(RightSensor.position,hit.point);
        }
        if (Physics.Raycast(LeftSensor.position, LeftSensor.forward, out hit, Mathf.Infinity, mask))
        {
            fLeftDiatance = Vector3.Distance(LeftSensor.position, hit.point);
        }
    }

    public bool IsDeadEnd(out bool rightWay)
    {
        rightWay = true;
        if (curTrans == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public Quaternion EyeDirX()
    {
        return EyeTrans.rotation;
    }

    public Quaternion EyeDirY()
    {
        return transform.rotation;
    }

    public bool FindedUnit(out GameObject find)
    {
        if (FindUnit.Count > 0)
        {
            find = FindUnit[0];
            return true;
        }
        else
        {
            find = null;
            return false;
        }
    }
}
