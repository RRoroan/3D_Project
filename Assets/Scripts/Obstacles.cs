using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public PlayerController controller;
    private void Awake()
    {
        controller = CharacterManager.Instance.Player.controller;
    }


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Calling Super");
            CharacterManager.Instance.Player.controller.SuperJumpReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player out");
            CharacterManager.Instance.Player.controller.SuperJumpReady = false;
        }
    }
}
