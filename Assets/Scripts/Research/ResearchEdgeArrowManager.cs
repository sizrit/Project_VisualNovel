using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public enum EdgeArrowDirection
{
    Left,
    Right,
    Up,
    Down
}

public class ResearchEdgeArrowManager : MonoBehaviour
{
    #region Singleton

    private static ResearchEdgeArrowManager _instance;

    public static ResearchEdgeArrowManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ResearchEdgeArrowManager>();
            if (obj == null)
            {
                Debug.LogError("Error! ResearchEdgeArrowManager is disable now");
                return null;
            }

            _instance = obj;
        }
        return _instance;
    }

    #endregion
    
    private enum ArrowState
    {
        Default,
        Idle,
        On,
        Off
    }

    [SerializeField] private GameObject researchObjectSetLoadManger;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;

    private readonly Dictionary<string, Sprite> _arrowImageList = new Dictionary<string, Sprite>();
    private static readonly int State = Animator.StringToHash("State");

    public void LoadImage()
    {
        Sprite[] imageList = Resources.LoadAll<Sprite>("ResearchArrow");
        foreach (var image in imageList)
        {
            _arrowImageList.Add(image.name,image);
        }
    }

    public void SetArrowOn(EdgeArrowDirection direction)
    {
        switch (direction)
        {
            case EdgeArrowDirection.Left:
                leftArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.On);
                return;
            
            case EdgeArrowDirection.Right:
                rightArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.On);
                return;
            
            case EdgeArrowDirection.Up:
                upArrow.GetComponent<Image>().sprite = _arrowImageList["Up_On"];
                return;
            
            case EdgeArrowDirection.Down:
                downArrow.GetComponent<Image>().sprite = _arrowImageList["Down_On"];
                return;
        }
    }
    
    public void SetArrowOff(EdgeArrowDirection direction)
    {
        switch (direction)
        {
            case EdgeArrowDirection.Left:
                leftArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.Off);
                return;
            
            case EdgeArrowDirection.Right:
                rightArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.Off);
                return;
            
            case EdgeArrowDirection.Up:
                upArrow.GetComponent<Image>().sprite = _arrowImageList["null"];
                return;
            
            case EdgeArrowDirection.Down:
                downArrow.GetComponent<Image>().sprite = _arrowImageList["null"];
                return;
        }
    }

    public void SetArrowIdle(EdgeArrowDirection direction)
    {
        switch (direction)
        {
            case EdgeArrowDirection.Left:
                leftArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.Idle);
                return;
            
            case EdgeArrowDirection.Right:
                rightArrow.GetComponent<Animator>().SetInteger(State,(int)ArrowState.Idle);
                return;
            
            case EdgeArrowDirection.Up:
                upArrow.GetComponent<Image>().sprite = _arrowImageList["Up_Idle"];
                return;
            
            case EdgeArrowDirection.Down:
                downArrow.GetComponent<Image>().sprite = _arrowImageList["Down_Idle"];
                return;
        }
    }

    public void SetAllArrowAuto()
    {
        GameObject objectSet = researchObjectSetLoadManger.transform.GetChild(0).gameObject;
        Rect rect = objectSet.GetComponent<RectTransform>().rect;
        var position = objectSet.transform.position;
        float x = position.x;
        float y = position.y;
        
        if (rect.width / 2.0f > 960f + x)
        {
            SetArrowIdle(EdgeArrowDirection.Left);
        }
        else
        {
            SetArrowOff(EdgeArrowDirection.Left);
        }

        if (rect.width / 2.0f > 960f - x)
        {
            SetArrowIdle(EdgeArrowDirection.Right);
        }
        else
        {
            SetArrowOff(EdgeArrowDirection.Right);
        }

        if (rect.height / 2.0f > 590f - y)
        {
            SetArrowIdle(EdgeArrowDirection.Up);
        }
        else
        {
            SetArrowOff(EdgeArrowDirection.Up);
        }

        if (rect.height / 2.0f > 590f + y)
        {
            SetArrowIdle(EdgeArrowDirection.Down);
        }
        else
        {
            SetArrowOff(EdgeArrowDirection.Down);
        }
    }
}
