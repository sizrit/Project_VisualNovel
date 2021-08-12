using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_MenuButton : MonoBehaviour
{
    private StoryBoardClickSystem _storyBoardClickSystem;

    private void OnEnable()
    {
        _storyBoardClickSystem =StoryBoardClickSystem.GetInstance();
        _storyBoardClickSystem.SubscribeUiCheckClick(CheckClick);
    }
    
    private void CheckClick(RaycastHit2D hit)
    {
        if (hit.transform == this.transform)
        {
            UI_GameMenuManager.GetInstance().InstantiateGameMenu();
            UI_GameMenuManager.GetInstance().SetMenuMode(MenuMode.Inventory);
            ClickSystem.GetInstance().SetClickMode(ClickMode.Menu);
        }
    }
}
