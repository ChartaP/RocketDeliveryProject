using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Camera MainCamera;
    private Vector3 CameraPos = new Vector3(0,2.2f,-2f);
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
        MainCamera.transform.RotateAround(target.position, target.right , Input.GetAxis("Mouse Y") * -0.5f);
        MainCamera.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * -0.5f);
    }

    public void TargetChange(Transform target)
    {
        this.target = target;
        MainCamera.transform.parent = target;
        MainCamera.transform.position = target.position + CameraPos;
    }
}
