using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed;
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
    }

    void Move()
    {
        float acceleration = 10f;
        Vector3 targetVelocity = (transform.forward * curMovementInput.y + transform.right * curMovementInput.x) * moveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * acceleration);

        //Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        //dir *= moveSpeed;
        //dir.y = rb.velocity.y;

        //rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
}
