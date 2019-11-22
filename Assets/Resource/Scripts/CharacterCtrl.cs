﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    
    protected Vector3 Move_Dir = Vector3.zero;

    public float fSpeed =5;
    public float fJump  =10;
    protected const float fGracity = 9.8f;

    [SerializeField]
    public float cameraHeight = 2;
    public float cameraDistance=2;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Ctrl(float X,float Y,float Z)
    {
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

}
