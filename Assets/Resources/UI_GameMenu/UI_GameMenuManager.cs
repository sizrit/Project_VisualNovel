using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameMenuManager : MonoBehaviour
{
    #region Singleton

    private static UI_GameMenuManager _instance;

    public static UI_GameMenuManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<UI_GameMenuManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                Debug.Log("Error! 'UI_GameMenuManager' is null");
            }
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<UI_GameMenuManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private UI_GameMenuClickSystem _uiGameMenuClickSystem;
    
    private GameObject _uiGameMenu;
    
    private GameObject _main;
    private GameObject _dialogueLogPrefab;
    private GameObject _inventoryPrefab;
    private GameObject _settingPrefab;

    private void OnEnable()
    {
        _uiGameMenuClickSystem = UI_GameMenuClickSystem.GetInstance();
        
        string loadPath = "UI_GameMenu/Prefabs/";
        _uiGameMenu = Resources.Load<GameObject>(loadPath + "UI_GameMenuPrefab");

        _dialogueLogPrefab = Resources.Load<GameObject>(loadPath + "DialogueLogPrefab");
        _inventoryPrefab = Resources.Load<GameObject>(loadPath + "ClueInventoryPrefab");
        _settingPrefab = Resources.Load<GameObject>(loadPath + "SettingPrefab");
    }

    public void InstantiateGameMenu()
    {
        Instantiate(_uiGameMenu, this.transform);
        _main = GameObject.Find("Main");
    }

    public void SetMenuMode(MenuMode mode)
    {
        _uiGameMenuClickSystem.ResetCheckClickList();
        switch (mode)
        {
            case MenuMode.DialogueLog:
                RemoveAllInMain();
                Instantiate(_dialogueLogPrefab, _main.transform);
                UI_GameMenu_DialogueLogManager.GetInstance().ShowDialogueLog();
                break;
            case MenuMode.Inventory:
                RemoveAllInMain();
                Instantiate(_inventoryPrefab, _main.transform);
                break;
            case MenuMode.Setting:
                RemoveAllInMain();
                Instantiate(_settingPrefab, _main.transform);
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

    public void RemoveGameMenu()
    {
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
