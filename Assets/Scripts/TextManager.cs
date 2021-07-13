using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private static TextManager _instance;

    public static TextManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject newObj = new GameObject("TextManager");
            _instance = newObj.AddComponent<TextManager>();
        }

        return _instance;
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<TextManager>();
        if (objs.Length != 1)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public void SetText(string stringValue)
    {
        this.GetComponent<Text>().text = stringValue;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
