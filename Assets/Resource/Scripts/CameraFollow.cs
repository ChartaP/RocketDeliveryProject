using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    private Vector3 CameraPos = new Vector3(0,2.2f,-2f);
    // Start is called before the first frame update
    void Start()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
            MainCamera.transform.parent = transform;
            MainCamera.transform.position = transform.position + CameraPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.RotateAround(transform.position, transform.right , Input.GetAxis("Mouse Y") * -0.5f);
        MainCamera.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * -0.5f);
    }
}
