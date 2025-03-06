using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerResource playerResource;
    private AnimationController animationController;
    private CharacterController characterController;

    [Header("Movement Setting")]
    public float speed;
    public float gravity = 9.81f;
    private float verticalVelocity;
    public float jumpHeight;
    private Vector3 curMovementInput;

    [Header("Look Setting")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;



    private void Awake()
    {
        playerResource = GetComponent<PlayerResource>();
        animationController = GetComponentInChildren<AnimationController>();
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        move *= speed;
        
        move.y = VerticalForceCalculation();
        characterController.Move(move * Time.deltaTime);
    }

    private float VerticalForceCalculation()
    {
        if (characterController.isGrounded) 
        {
            verticalVelocity += -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime * 4f;
        }
        return verticalVelocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            animationController.Move(); 
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animationController.Stop();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
            animationController.Jump();
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

}
