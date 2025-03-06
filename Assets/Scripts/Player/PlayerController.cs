using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerResource playerResource;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public float extraGravity;
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
    }

    void Move()
    {
        //Using Vector3.Lerp > making the player movement more smooth while moving after player stopped
        float acceleration = 20f;
        Vector3 targetVelocity = (transform.forward * curMovementInput.y + transform.right * curMovementInput.x) * moveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * acceleration);
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
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (playerResource.uiResource.stamina.curValue >= 20f)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode.VelocityChange);
                playerResource.uiResource.stamina.Subtract(20);
            }
        }
    }

    void ExtraGravity()
    {
        rb.AddForce(Vector2.down * extraGravity, ForceMode.VelocityChange);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
