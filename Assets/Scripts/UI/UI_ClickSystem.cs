using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClickSystem : MonoBehaviour
{

    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);
            
            foreach (var hit in hitList)
            {
                if (hit.transform == GameObject.Find("Back").transform)
                {
                    EndUI_Menu();
                }
            }
            
        }
    }

    private void EndUI_Menu()
    {
        GameObject.Find("UI _Menu").transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Click();
    }
}
