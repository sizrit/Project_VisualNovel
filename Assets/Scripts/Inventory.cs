using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

            bool isCheck = false;
            foreach (var hit in hitList)
            {
                if (hit.transform == this.gameObject.transform)
                {
                    isCheck = true;
                }
            }

            if (!isCheck)
            {
                GameSystem.GetInstance().PauseOff();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        Click();
    }
}
