using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    private CharacterController CharCtrl = null;
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
        CharCtrl = transform.GetComponent<CharacterController>();
        Move_Dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Ctrl(float X,float Y,float Z)
    {
        if (CharCtrl.isGrounded)
        {
            Move_Dir = new Vector3(X, 0, Z);
            Move_Dir = transform.TransformDirection(Move_Dir);
            Move_Dir *= fSpeed;

            Move_Dir.y = Y * fJump;
        }

        Move_Dir.y -= fGracity * Time.deltaTime;

        CharCtrl.Move(Move_Dir * Time.deltaTime);
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

}
