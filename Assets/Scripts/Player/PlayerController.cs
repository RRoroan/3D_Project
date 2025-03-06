using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerResource playerResource;
    public AnimationController animationController;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public float extraGravity;
    public bool isGround;
    public bool isJumping;
    public LayerMask layerMask;
    private Vector2 curMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerResource = GetComponent<PlayerResource>();
        animationController = GetComponentInChildren<AnimationController>();
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
        ExtraGravity();
        CheckGround();
    }

    void Move()
    {
        //Using Vector3.Lerp > making the player movement more smooth while moving after player stopped
        float acceleration = 20f;
        Vector3 targetVelocity = (transform.forward * curMovementInput.y + transform.right * curMovementInput.x) * moveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * acceleration);
    }

    void CheckGround()
    {
        float checkDistance = 0.5f;
        isGround = Physics.Raycast(transform.position, Vector3.down, checkDistance, layerMask);
        if (isGround)
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
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
        if(context.phase == InputActionPhase.Started && isGround)
        {
            if (playerResource.uiResource.stamina.curValue >= 20f)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode.VelocityChange);
                playerResource.uiResource.stamina.Subtract(20);
                animationController.Jump();
                isJumping = true;
            }
        }
    }

    void ExtraGravity()
    {
        if (!isJumping)
        {
            float gravityMultiplier = isJumping ? 0.5f : 2f;
            rb.AddForce(Vector3.down * extraGravity * gravityMultiplier, ForceMode.Acceleration);
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
