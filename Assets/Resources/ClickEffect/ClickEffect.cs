using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    private float timer = 0;
    private float endTime = 1;
    
    private void OnEnable()
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > endTime)
        {
            Destroy(this.gameObject);
        }
    }
}
