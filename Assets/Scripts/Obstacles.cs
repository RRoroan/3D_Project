using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public PlayerController controller;
    public SphereCollider sphereCollider;
    public Player player;
    [SerializeField] private Transform jumpSpring;
    private Vector3 originPos;
    private void Awake()
    {
        player = CharacterManager.Instance.Player;
        controller = player.controller;
        sphereCollider = GetComponent<SphereCollider>();
        originPos = jumpSpring.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.controller.SuperJumpReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.controller.SuperJumpReady = false;
            JumpSpringDown();
        }
    }

    public void JumpSpringUp()
    {
        StartCoroutine(MoveJumpSpring(jumpSpring.transform.position, new Vector3(jumpSpring.transform.position.x, 225f, jumpSpring.transform.position.z)));
    }

    public void JumpSpringDown()
    {
        StartCoroutine(MoveJumpSpring(jumpSpring.position, originPos));
    }

    private IEnumerator MoveJumpSpring(Vector3 start, Vector3 end)
    {
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            jumpSpring.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        jumpSpring.transform.position = end;
    }
}
