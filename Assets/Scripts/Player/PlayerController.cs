using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; //InputAction을 사용하기 위한 using

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float JumpPower;
    private Vector2 curMovementInput; //inputAction에서 받아올 값
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;


    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스 모양을 숨기는 방법
    }
    void FixedUpdate()//물리연산은 여기서 하면 좋음
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraLook();
    }
    void Move()
    {
        //W,A,S,D의 값
        Vector3 dir = transform.forward*curMovementInput.y+transform.right*curMovementInput.x ;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook() 
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        //최솟값최대값을 넘어가지 않게clamp
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //이벤트 등록을 위한 움직임 정보를 받는 함수 생성
    public void OnMove(InputAction.CallbackContext context) 
    {
        //context는 현재 상태를 받아올수 있다. phase는 분기점을 나타낸다고 생각됨
        //만약 현재 분기점과 키입력이 눌린 상태라면
        if (context.phase == InputActionPhase.Performed) //Started는 한번누르고 그 뒤에 눌러도 값을 받아오지 않음
        { 
            curMovementInput=context.ReadValue<Vector2>(); //inputAction에서 받아올 값에 현재의 값을 저장해라
        }
        else if(context.phase == InputActionPhase.Canceled) //키입력이 취소 되었을떄
        {
            curMovementInput = Vector2.zero;
        }

    }

    public void OnLook(InputAction.CallbackContext context) 
    {
        mouseDelta =  context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector2.up*JumpPower,ForceMode.Impulse);
        }
    }

    bool IsGround() 
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position+(transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(-transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(transform.right*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position+(-transform.right*0.2f)+(transform.up*0.01f),Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++) 
        {
            if (Physics.Raycast(rays[i],0.1f,groundLayerMask))

            { 
                return true;
            }
        }
        return false;
    }
}
