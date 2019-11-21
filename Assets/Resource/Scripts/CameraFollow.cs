using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Camera MainCamera;
    private Vector3 CameraPos = new Vector3(0,2.2f,2f);
    // Start is called before the first frame update
    void Start()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
            return;
        MainCamera.transform.position = target.position;
        target.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * 1.0f);
        MainCamera.transform.position -= Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, 0) * Vector3.forward * CameraPos.z ;
        MainCamera.transform.position += new Vector3(0,CameraPos.y,0);
        MainCamera.transform.LookAt(target.position);
    }

    public void SetPosition(float Height,float distance)
    {
        CameraPos.y = Height;
        CameraPos.z = distance;
    }
    
}
