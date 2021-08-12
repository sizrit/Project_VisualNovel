using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameMenu_Setting : MonoBehaviour
{
    private void OnEnable()
    {
        UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
    }

    private void CheckClick(RaycastHit2D hit)
    {
        Debug.Log("setting click");
    }
}
