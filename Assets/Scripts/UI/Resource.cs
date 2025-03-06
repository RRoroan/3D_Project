using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public Image uiBar;
    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(int value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(int value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
