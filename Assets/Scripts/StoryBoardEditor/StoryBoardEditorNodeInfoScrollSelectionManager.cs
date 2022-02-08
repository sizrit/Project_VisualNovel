using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    public class StoryBoardEditorNodeInfoScrollSelectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject mask;
        [SerializeField] private GameObject contentText;
        [SerializeField] private GameObject boxColliderLayer;
        [SerializeField] private GameObject boxCollider;
        private GameObject _mainText;

        private const float Width = 60;

        List<string> _contentTextList = new List<string>();

        private bool CheckMaskArea(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == mask)
                {
                    return true;
                }
            }
            return false;
        }

        private void EndScrollSelection()
        {
            StoryBoardEditorNodeInfoManager.GetInstance().SelectionModeOff();
            Destroy(this.gameObject);
        }

        public void SetScrollSelection<T>(List<T> contents, GameObject mainText)
        {
            StoryBoardEditorNodeInfoManager.GetInstance().SelectionModeOn(CheckClick);
            _mainText = mainText;

            _contentTextList = new List<string>();
            for (int i = 0; i < contents.Count; i++)
            {
                _contentTextList.Add(contents[i].ToString());
                GameObject boxColliderObject = Instantiate(this.boxCollider, boxColliderLayer.transform);
                boxColliderObject.name = "BoxCollider" + contents.IndexOf(contents[i]);
                boxColliderObject.transform.localPosition = new Vector3(0, -1 * Width * i, 0);
            }

            SetContentText();
            SetContentTextPosition();
        }

        private void SetContentTextPosition()
        {
            float height = _contentTextList.Count * 60;
            float maskH = mask.GetComponent<RectTransform>().rect.height;
            float y = height - maskH - _contentTextList.Count * Width / 2f;
            contentText.transform.localPosition = new Vector3(0, y, 0);
        }

        private void SetContentText()
        {
            string toString = "";
            foreach (var content in _contentTextList)
            {
                toString += content;
                if (_contentTextList.IndexOf(content) != _contentTextList.Count - 1)
                {
                    toString += "\n";
                }
            }

            contentText.GetComponent<Text>().text = toString;
        }

        private void CheckContent(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                for (int i = 0; i < _contentTextList.Count; i++)
                {
                    if (hit.transform == boxColliderLayer.transform.GetChild(i))
                    {
                        _mainText.transform.GetComponentInChildren<Text>().text = _contentTextList[i];
                    }
                }
            }
        }

        private void CheckClick(RaycastHit2D[]hits)
        {
            if (CheckMaskArea(hits))
            {
                CheckContent(hits);
            }

            EndScrollSelection();
        }

        private void CheckScroll()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                float height = _contentTextList.Count * Width;
                float maskH = mask.GetComponent<RectTransform>().rect.height;

                if (contentText.GetComponent<RectTransform>().anchoredPosition.y >
                    maskH - height + _contentTextList.Count * Width / 2f)
                {
                    contentText.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, maskH - height + _contentTextList.Count * Width / 2f);
                    return;
                }

                if (contentText.GetComponent<RectTransform>().anchoredPosition.y <
                    height - maskH - _contentTextList.Count * Width / 2f)
                {
                    contentText.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, height - maskH - _contentTextList.Count * Width / 2f);
                    return;
                }

                contentText.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, Input.mouseScrollDelta.y);
            }
        }

        private void Update()
        {
            CheckScroll();
        }
    }
}
