using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    #region Singleton

    private static PauseMenuManager _instance;

    public static PauseMenuManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<PauseMenuManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                Debug.Log("Error! MenuUILoadManager is null");
            }
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<PauseMenuManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private GameObject _main;
    private GameObject _dialogueLogPrefab;
    private GameObject _inventoryPrefab;

    private MenuMode _currentMode = MenuMode.Inventory;
    
    private void OnEnable()
    {
        _main = GameObject.Find("Main");
        
        string loadPath = "MenuUI/Prefabs/";
        _dialogueLogPrefab = Resources.Load<GameObject>(loadPath + "DialogueLogPrefab");
        _inventoryPrefab = Resources.Load<GameObject>(loadPath + "InventoryPrefab");
    }

    public void SetMenuMode(MenuMode mode)
    {
        RemoveAllInMain();
        switch (mode)
        {
            case MenuMode.DialogueLog:
                Instantiate(_dialogueLogPrefab, _main.transform);
                break;
            case MenuMode.Inventory:
                Instantiate(_inventoryPrefab, _main.transform);
                break;
        }
    }

    public void RemoveAllInMain()
    {
        for (int i = 0; i < _main.transform.childCount; i++)
        {
            Destroy(_main.transform.GetChild(i).gameObject);
        }
    }
}
