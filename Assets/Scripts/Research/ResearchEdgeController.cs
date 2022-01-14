using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchEdgeController : MonoBehaviour
{
    #region Singleton

    private static ResearchEdgeController _instance;

    public static ResearchEdgeController GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ResearchEdgeController>();
            if (obj == null)
            {
                Debug.LogError("Error! ResearchEdgeController is disable now");
                return null;
            }

            _instance = obj;
        }
        return _instance;
    }

    #endregion
    
    [SerializeField] private GameObject researchObjectSetLoadManger;
    [SerializeField] private float speed = 10;

    readonly List<string> _needEdgeControlResearchIdList = new List<string>();
    private bool _isEdgeControllerActive = true;

    public void LoadEdgeControlData()
    {
        _needEdgeControlResearchIdList.Add("R001");
        DisableEdgeControl();
    }

    public void SetEdgeControl(string researchId)
    {
        if (_needEdgeControlResearchIdList.Contains(researchId))
        {
            EnableEdgeControl();
            return;
        }
        DisableEdgeControl();
    }

    public void EnableEdgeControl()
    {
        _isEdgeControllerActive = true;
    }
    
    public void DisableEdgeControl()
    {
        _isEdgeControllerActive = false;
    }

    private void EdgeControl()
    {
        if (_isEdgeControllerActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

            if (hits.Length == 0)
            {
                ResearchEdgeArrowManager.GetInstance().SetAllArrowAuto();
            }

            foreach (var hit in hits)
            {
                MoveObjectSet(hit.transform.name);
            }
        }
    }

    private void Update()
    {
        EdgeControl();
    }


    private void MoveObjectSet(string arrowName)
    {
        GameObject objectSet = researchObjectSetLoadManger.transform.GetChild(0).gameObject;
        Rect rect = objectSet.GetComponent<RectTransform>().rect;
        var position = objectSet.transform.position;
        float x = position.x;
        float y = position.y;
        
        switch (arrowName)
        {
            case "R_LeftEdge":
                if (rect.width / 2.0f > 960f + x)
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOn(EdgeArrowDirection.Left);
                    ResearchEdgeArrowManager.GetInstance().SetArrowIdle(EdgeArrowDirection.Right);
                    objectSet.transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOff(EdgeArrowDirection.Left);
                    objectSet.transform.position = new Vector3(rect.width / 2.0f - 960f, y, +100);
                }
                break;
                    
            case "R_RightEdge":
                if (rect.width / 2.0f > 960f - x)
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOn(EdgeArrowDirection.Right);
                    ResearchEdgeArrowManager.GetInstance().SetArrowIdle(EdgeArrowDirection.Left);
                    objectSet.transform.position += new Vector3(-1f * speed, 0, 0);
                }
                else
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOff(EdgeArrowDirection.Right);
                    objectSet.transform.position = new Vector3(-1f * (rect.width / 2.0f - 960f), y, +100);
                }
                break;
                
            case "R_TopEdge":
                if (rect.height / 2.0f > 590f - y)
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOn(EdgeArrowDirection.Up);
                    ResearchEdgeArrowManager.GetInstance().SetArrowIdle(EdgeArrowDirection.Down);
                    objectSet.transform.position += new Vector3(0, -1 * speed, 0);
                }
                else
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOff(EdgeArrowDirection.Up);
                    objectSet.transform.position = new Vector3(x, -1f * (rect.height / 2.0f - 590f), +100);
                }
                break;
                
            case "R_BottomEdge":
                if (rect.height / 2.0f > 590f + y)
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOn(EdgeArrowDirection.Down);
                    ResearchEdgeArrowManager.GetInstance().SetArrowIdle(EdgeArrowDirection.Up);
                    objectSet.transform.position += new Vector3(0, speed, 0);
                }
                else
                {
                    ResearchEdgeArrowManager.GetInstance().SetArrowOff(EdgeArrowDirection.Down);
                    objectSet.transform.position = new Vector3(x, rect.height / 2.0f - 590f, +100);
                }
                break;
                    
            default:
                ResearchEdgeArrowManager.GetInstance().SetAllArrowAuto();
                break;
        }
    }
}
