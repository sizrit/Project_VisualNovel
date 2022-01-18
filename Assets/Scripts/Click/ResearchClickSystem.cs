using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchClickSystem : I_ClickSystem
{
    #region Singleton

    private static ResearchClickSystem _instance;

    public static ResearchClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ResearchClickSystem();
        }
        return _instance;
    }

    #endregion

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
            
            foreach (var hit in hits)
            {
                hit.transform.GetComponent<IResearchClickable>().Click();
            }
        }
    }
}
