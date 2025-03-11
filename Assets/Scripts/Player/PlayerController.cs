using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerResource playerResource;
    private AnimationController animationController;
    private CharacterController characterController;
    [SerializeField] private Obstacles obstacles;

    [Header("Movement Setting")]
    public float walkSpeed;
    public float gravity = 9.81f;
    public float sprintSpeed;
    public float sprintTransitSpeed;
    private float verticalVelocity;
    public float jumpHeight;
    private Vector3 curMovementInput;
    private float speed;
    public float superJumpHeight;
    public bool SuperJumpReady;

    [Header("Look Setting")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;

    private void Awake()
    {
        playerResource = GetComponent<PlayerResource>();
        animationController = GetComponentInChildren<AnimationController>();
        characterController = GetComponent<CharacterController>();
        obstacles = FindObjectOfType<Obstacles>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SuperJumpReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        Check();
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

    public void Check()
    {
        Ray ray = new Ray(transform.position + (transform.up * 3f) + (transform.forward * 3f), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f);
        }

    }

    void Move()
    {
        Vector3 move = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (playerResource.uiResource.stamina.curValue > 0)
            {
                speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
                playerResource.uiResource.stamina.Subtract(60 * Time.deltaTime);
            }
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }
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

    public void SuperJump(InputAction.CallbackContext context)
    {
        if (SuperJumpReady)
        {
            if (context.phase == InputActionPhase.Started)
            {
                verticalVelocity = Mathf.Sqrt(2f * gravity * superJumpHeight);
                animationController.Jump();
                obstacles.JumpSpringUp();
            }
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

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
        }
    }

}
