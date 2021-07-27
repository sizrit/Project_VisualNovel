using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public void Click()
    {
        GameSystem.GetInstance().PauseOn();
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
