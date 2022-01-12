using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_MenuButton : MonoBehaviour
{
    private void OnEnable()
    {
        StoryBoardClickSystem.GetInstance().SubscribeUiCheckClick(CheckClick);
    }
    
    private void CheckClick(RaycastHit2D hit)
    {
        if (hit.transform == this.transform)
        {
            UI_GameMenuManager.GetInstance().Show_UI_GameMenu();
            UI_GameMenuManager.GetInstance().SetMenuMode(UiMenuMode.Inventory);
            ClickSystem.GetInstance().SetClickMode(ClickMode.Menu);
        }
    }
}
