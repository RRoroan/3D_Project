using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public MeshCollider[] meshColliders;
    public Transform jumpSpring;

    private void Awake()
    {
        meshColliders = GetComponentsInChildren<MeshCollider>();
    }

    private void Update()
    {
        Jump();
    }

    bool CheckPlayer()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * 5f), Vector3.up),
            new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * 5f), Vector3.up),
            new Ray(transform.position + (transform.right * 0.5f) + (transform.up * 5f), Vector3.up),
            new Ray(transform.position + (-transform.right * 0.5f) + (transform.up * 5f), Vector3.up)
        };

        foreach (Ray ray in rays)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        }
        return false;
    }

    bool PlayerAccept()
    {
        if (Input.GetKey(KeyCode.E))
            return true;
        else return false;
    }

    public void Jump()
    {
        if (PlayerAccept() && CheckPlayer())
        {
            Debug.Log("JUMP");
        }
    }
}
