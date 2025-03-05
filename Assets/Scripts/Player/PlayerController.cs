using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public float extraGravity;
    private Vector2 curMovementInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        ExtraGravity();
    }

    void Move()
    {
        //Using Vector3.Lerp > making the player movement more smooth while moving after player stopped
        float acceleration = 5f;
        Vector3 targetVelocity = (transform.forward * curMovementInput.y + transform.right * curMovementInput.x) * moveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * acceleration);
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
            rb.AddForce(Vector2.up * jumpPower, ForceMode.VelocityChange);
        }
    }

    void ExtraGravity()
    {
        rb.AddForce(Vector2.down * extraGravity, ForceMode.VelocityChange);
    }
}
