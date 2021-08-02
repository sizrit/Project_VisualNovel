using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
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
            GameObject.Find("UI _Menu").transform.GetChild(0).gameObject.SetActive(true);
            ClickSystem.GetInstance().SetClickMode(ClickMode.Menu);
        }
    }
}
