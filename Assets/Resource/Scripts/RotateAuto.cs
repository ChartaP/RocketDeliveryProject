using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAuto : MonoBehaviour
{
    public Vector3 Dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Dir * Time.deltaTime);
    }
}
