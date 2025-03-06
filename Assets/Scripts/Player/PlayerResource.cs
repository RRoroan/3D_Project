using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public UIResource uiResource;

    Resource health { get { return uiResource.health; } }
    Resource stamina {  get { return uiResource.stamina; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
    }
}
