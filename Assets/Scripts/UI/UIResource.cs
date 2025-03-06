using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResource : MonoBehaviour
{
    public Resource health;
    public Resource stamina;

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.resource.uiResource = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
