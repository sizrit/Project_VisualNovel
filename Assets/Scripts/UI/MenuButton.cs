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
        _storyBoardClickSystem.SetCheckClickPreset(CheckClick,StoryBoardCheckClickPreSet.Menu);
        _storyBoardClickSystem.SubscribeCheckClick(StoryBoardCheckClickPreSet.Menu);
    }

    private void CheckClick(RaycastHit2D hit)
    {
        if (hit.transform == this.transform)
        {
            _storyBoardClickSystem.SubscribeCheckClick(Click);
        }
    }

    private void Click(RaycastHit2D hit)
    {
        if (hit.transform == GameObject.Find("Back").transform)
        {
            GameObject.Find("UI _Menu").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
