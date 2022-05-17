using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UI_GameMenu;
using UnityEngine;

namespace ClickSystem
{
    public enum UiMenuMode
    {
        DialogueLog,
        Inventory,
        ClueInventory,
        Setting
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UI_GameMenuClickSystem : I_ClickSystem
    {
        #region Singleton

        private static UI_GameMenuClickSystem _instance;

        public static UI_GameMenuClickSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance=new UI_GameMenuClickSystem();
            }
            return _instance;
        }

        #endregion
    
        private UiMenuMode _currentMode = UiMenuMode.Inventory;

        readonly List<Action<RaycastHit2D>> _checkClickList = new List<Action<RaycastHit2D>>();

        public void SubScribeCheckClickFunc(Action<RaycastHit2D> checkClickDelegate)
        {
            if (!_checkClickList.Contains(checkClickDelegate))
            {
                _checkClickList.Add(checkClickDelegate);
            }
        }

        public void UnSubscribeCheckClick(Action<RaycastHit2D> checkClickDelegate)
        {
            if (_checkClickList.Contains(checkClickDelegate))
            {
                _checkClickList.Remove(checkClickDelegate);
            }
        }

        public void Click()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

                foreach (var hit in hitList)
                {
                    CheckTabMenuClick(hit);
                    CheckClick(hit);
                }
            }
        }

        private void CheckClick(RaycastHit2D hit)
        {
            Action<RaycastHit2D> tempDelegate = delegate { };
            foreach (var checkClick in _checkClickList)
            {
                tempDelegate += checkClick;
            }

            tempDelegate(hit);
        }

        private void CheckTabMenuClick(RaycastHit2D hit)
        {
            switch (hit.transform.name)
            {
                case "Inventory":
                    if (_currentMode != UiMenuMode.Inventory)
                    {
                        _currentMode = UiMenuMode.Inventory;
                        UI_GameMenuManager.GetInstance().SetMenuMode(UiMenuMode.Inventory);
                    }
                    break;
            
                case "ClueInventory":
                    if (_currentMode != UiMenuMode.ClueInventory)
                    {
                        _currentMode = UiMenuMode.ClueInventory;
                        UI_GameMenuManager.GetInstance().SetMenuMode(UiMenuMode.ClueInventory);
                    }
                    break;
            
                case "DialogueLog":
                    if (_currentMode != UiMenuMode.DialogueLog)
                    {
                        _currentMode = UiMenuMode.DialogueLog;
                        UI_GameMenuManager.GetInstance().SetMenuMode(UiMenuMode.DialogueLog);
                    }
                    break;
            
                case "Setting":
                    if (_currentMode != UiMenuMode.Setting)
                    {
                        _currentMode = UiMenuMode.Setting;
                        UI_GameMenuManager.GetInstance().SetMenuMode(UiMenuMode.Setting);
                    }
                
                    break;
            
                case "Back":
                    _currentMode = UiMenuMode.Inventory;
                    UI_GameMenuManager.GetInstance().Hide_UI_GameMenu();
                    global::ClickSystem.ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
                    break;
            }
        }
    }
}