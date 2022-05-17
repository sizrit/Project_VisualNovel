using System.Diagnostics.CodeAnalysis;
using ClickSystem;
using UnityEngine;

namespace UI_GameMenu
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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

        [SerializeField] private GameObject uiGameMenuObject;
        [SerializeField] private GameObject dialogueLogPrefab;
        [SerializeField] private GameObject inventoryPrefab;
        [SerializeField] private GameObject clueInventoryPrefab;
        [SerializeField] private GameObject settingPrefab;

        public void Show_UI_GameMenu()
        {
            RemoveAllInMain();
            uiGameMenuObject.SetActive(true);
        }

        public void SetMenuMode(UiMenuMode mode)
        {
            Transform main = uiGameMenuObject.transform.GetChild(2);
            RemoveAllInMain();
            switch (mode)
            {
                case UiMenuMode.DialogueLog:
                    Instantiate(dialogueLogPrefab, main.transform);
                    UI_GameMenu_DialogueLogManager.GetInstance().ShowDialogueLog();
                    break;
                case UiMenuMode.Inventory:
                    Instantiate(inventoryPrefab, main.transform);
                    break;
                case UiMenuMode.ClueInventory:
                    Instantiate(clueInventoryPrefab, main.transform);
                    break;
                case UiMenuMode.Setting:
                    Instantiate(settingPrefab, main.transform);
                    break;
            }
        }

        private void RemoveAllInMain()
        {
            Transform main = uiGameMenuObject.transform.GetChild(2);
            for (int i = 0; i < main.transform.childCount; i++)
            {
                Destroy(main.transform.GetChild(i).gameObject);
            }
        }

        public void Hide_UI_GameMenu()
        {
            uiGameMenuObject.SetActive(false);
        }
    }
}
