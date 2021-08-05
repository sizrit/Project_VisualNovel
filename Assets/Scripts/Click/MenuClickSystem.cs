using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickSystem : I_ClickSystem
{
    #region Singleton

    private static MenuClickSystem _instance;

    public static MenuClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance=new MenuClickSystem();
        }
        return _instance;
    }

    #endregion

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);
            GameObject backButton = GameObject.Find("Back");
            
            foreach (var hit in hitList)
            {
                
                if (backButton != null)
                {
                    if (hit.transform == backButton.transform)
                    {
                        EndUI_Menu();
                    }
                }
            }
        }
    }

    private void EndUI_Menu()
    {
        GameObject.Find("UI _Menu").transform.GetChild(0).gameObject.SetActive(false);
        ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
    }

    // Update is called once per frame
    public void Update()
    {
        Click();
    }
}
