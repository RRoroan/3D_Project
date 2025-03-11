using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public Transform bridge;
    private Vector3 originPos;
    public Vector3 targetPos;

    private void Start()
    {
        bridge = GetComponent<Transform>();
        originPos = bridge.position;
        if (bridge == null) bridge = transform;
        originPos = bridge.position;
        targetPos = new Vector3(originPos.x, originPos.y - 50f, originPos.z); 

        StartCoroutine(BridgeLoop());
    }

    IEnumerator BridgeLoop()
    {
        float duration = 3f; 

        while (true) 
        {
            yield return StartCoroutine(MoveBridge(originPos, targetPos, duration)); 
            yield return StartCoroutine(MoveBridge(targetPos, originPos, duration)); 
        }
    }

    IEnumerator MoveBridge(Vector3 startP, Vector3 endP, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            bridge.position = Vector3.Lerp(startP, endP, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        bridge.position = endP; 
    }
}
