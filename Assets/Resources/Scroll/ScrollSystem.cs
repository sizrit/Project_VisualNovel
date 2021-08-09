using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSystem : MonoBehaviour
{
    private GameObject _content;

    private bool _lockScrollDown = false;
    private bool _lockScrollUp = false;
    
    private void OnEnable()
    {
        _content = this.transform.GetChild(0).gameObject;
    }

    private void ResetLock()
    {
        _lockScrollDown = false;
        _lockScrollUp = false;
    }

    private void CheckScroll()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

        foreach (var hit in hitList)
        {
            if (hit.transform == this.transform)
            {
                Scroll();
            }
        }
    }

    private void Scroll()
    {
        float contentY = _content.transform.localPosition.y;
        float contentH = _content.GetComponent<RectTransform>().rect.height;
        float viewH = this.GetComponent<RectTransform>().rect.height;
        if (contentY > contentH / 2f - viewH / 2f)
        {
            _lockScrollDown = true;
        }

        if (contentY < -1 * contentH / 2f + viewH / 2f)
        {
            _lockScrollUp = true;
        }

        Vector2 d = Input.mouseScrollDelta;
        //Debug.Log(d);
        Vector3 vec3 = new Vector3(0, d.y*5, 0);

        if (!_lockScrollDown && d.y > 0)
        {
            _lockScrollUp = false;
            this.transform.GetChild(0).localPosition += vec3;
        }

        if (!_lockScrollUp && d.y < 0)
        {
            _lockScrollDown = false;
            this.transform.GetChild(0).localPosition += vec3;
        }
    }

    public void SetPosition()
    {
        _content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        float contentH = _content.GetComponent<RectTransform>().rect.height;
        float viewH = this.GetComponent<RectTransform>().rect.height;
        this.transform.GetChild(0).localPosition = new Vector3(0,contentH / 2f - viewH / 2f +0.1f,0);
        ResetLock();
    }

    private void Update()
    {
        CheckScroll();
    }
}
