using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("Move");
    private static readonly int IsJump = Animator.StringToHash("Jump");
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move()
    {
        animator.SetBool(IsMove, true);
    }

    public void Stop()
    {
        animator.SetBool(IsMove, false);
    }

    public void Jump()
    {
        animator.SetTrigger(IsJump);
    }
}
