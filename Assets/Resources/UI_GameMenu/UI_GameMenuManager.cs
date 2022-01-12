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

    [SerializeField] private GameObject uiGameMenuPrefab;
    [SerializeField] private GameObject dialogueLogPrefab;
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private GameObject clueInventoryPrefab;
    [SerializeField] private GameObject settingPrefab;

    public void InstantiateGameMenu()
    {
        Instantiate(uiGameMenuPrefab, this.transform);
    }

    public void SetMenuMode(UiMenuMode mode)
    {
        Transform main = uiGameMenuPrefab.transform.GetChild(2);
        switch (mode)
        {
            case UiMenuMode.DialogueLog:
                RemoveAllInMain();
                Instantiate(dialogueLogPrefab, main.transform);
                UI_GameMenu_DialogueLogManager.GetInstance().ShowDialogueLog();
                break;
            case UiMenuMode.Inventory:
                RemoveAllInMain();
                Instantiate(inventoryPrefab, main.transform);
                break;
            case UiMenuMode.ClueInventory:
                RemoveAllInMain();
                Instantiate(clueInventoryPrefab, main.transform);
                break;
            case UiMenuMode.Setting:
                RemoveAllInMain();
                Instantiate(settingPrefab, main.transform);
                break;
        }
    }

    public void RemoveAllInMain()
    {
        Transform main = uiGameMenuPrefab.transform.GetChild(2);
        for (int i = 0; i < main.transform.childCount; i++)
        {
            Destroy(main.transform.GetChild(i).gameObject);
        }
    }

    public void RemoveGameMenu()
    {
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
