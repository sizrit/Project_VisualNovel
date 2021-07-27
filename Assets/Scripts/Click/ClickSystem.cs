using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    #region Singleton

    private static ClickSystem _instance;

    public static ClickSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ClickSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameSystem");
                _instance = gameObject.AddComponent<ClickSystem>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<ClickSystem>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }
    
    #endregion
    
    delegate void ClickDelegate();

    private ClickDelegate _clickDelegate;
    private StoryBoardManager _storyBoardManager;

    private void Func0(){}
    
    private void OnEnable()
    {
        _storyBoardManager = StoryBoardManager.GetInstance();
        
        _clickDelegate = new ClickDelegate(Func0);
        _clickDelegate += Click;
        
        
    }

    public void DisableClick()
    {
        _clickDelegate =new ClickDelegate(Func0);
    }
    
    public void EnableClick()
    {
        _clickDelegate += Click;
    }
    
    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.transform != null)
            {
                switch (hit.transform.tag)
                {
                    case "StoryBoard":
                        _storyBoardManager.StoryBoardClick();
                        break;
                
                    case "Inventory":
                        GameObject.Find("InventoryButton").transform.GetChild(0).gameObject.SetActive(true);
                        GameSystem.GetInstance().PauseOn();
                        break;
                }
            }
        }
    }

    private void Update()
    {
        _clickDelegate();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameSystem.GetInstance().PauseOn();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameSystem.GetInstance().PauseOff();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
