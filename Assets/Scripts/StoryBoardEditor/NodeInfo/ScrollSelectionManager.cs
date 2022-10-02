using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    [SuppressMessage("ReSharper", "Unity.PreferNonAllocApi")]
    public class ScrollSelectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject mask;
        [SerializeField] private GameObject boxColliderLayer;
        [SerializeField] private GameObject boxCollier2DPrefab;
        [SerializeField] private GameObject onCursorEffect;

        [SerializeField] [Range(10f, 30f)] private float scrollSpeed = 20f;

        private Action<string> _callBackFunc = delegate { };

        private const float Height = 60;
        private const float Width = 200;

        private readonly Dictionary<GameObject, string> boxCollider2DList = new Dictionary<GameObject, string>();

        private bool CheckMaskArea(RaycastHit2D[] hits) // 유저 Input 이 설정된 Mask 안에서 이루어졌는지 체크
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

        private void EndScrollSelection() // 종료시 gameObject 제거
        {
            Destroy(this.gameObject);
        }

        public void SetScrollSelection<T>(List<T> contents, Action<string> callBackFunc) // ScrollSelection 에 넣을 Data 전달
        {
            _callBackFunc = callBackFunc;

            for (int i = 0; i < contents.Count; i++)
            {
                string content = contents[i].ToString();
                GameObject boxColliderObject = Instantiate(boxCollier2DPrefab, boxColliderLayer.transform);
                boxColliderObject.name = "BoxCollider2D" + contents.IndexOf(contents[i]);
                boxColliderObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
                boxColliderObject.transform.localPosition = new Vector3(0, -1 * Height * i - Height / 2f, 0);
                boxColliderObject.GetComponent<BoxCollider2D>().size = new Vector2(Width, Height);
                boxColliderObject.GetComponent<Text>().text = content;
                boxCollider2DList.Add(boxColliderObject, content);
            }
        }

        private void CheckContent(RaycastHit2D[] hits) // 유저 Input 이 설정된 Content 와 상호작용 했는지 체크
        {
            foreach (var hit in hits)
            {
                foreach (var boxCollider2D in boxCollider2DList)
                {
                    if (hit.transform == boxCollider2D.Key.transform)
                    {
                        _callBackFunc(boxCollider2D.Value);
                    }
                }
            }
        }

        private void CheckClick() // 마우스 클릭 Input 을 확인
        {
            if (Input.GetMouseButtonDown(0) && Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                if (CheckMaskArea(hits))
                {
                    CheckContent(hits);
                }

                EndScrollSelection();
            }
        }

        private void CheckScroll() // 마우스 Scroll Input 을 확인
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                float boxH = boxCollider2DList.Count * Height;
                float maskH = mask.GetComponent<RectTransform>().rect.height;

                boxColliderLayer.GetComponent<RectTransform>().anchoredPosition -=
                    new Vector2(0, Input.mouseScrollDelta.y * scrollSpeed);

                float boxY = boxColliderLayer.GetComponent<RectTransform>().anchoredPosition.y;

                if (boxY < maskH / 2f)
                {
                    boxColliderLayer.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, maskH / 2f);
                }

                if (boxY > boxH - maskH / 2f)
                {
                    boxColliderLayer.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, boxH - maskH / 2f);
                }
            }
        }

        private void OnCursorEffect() // 마우스 커서가 올려져 있는 컴포넌트 하이라이트
        {
            Color color = onCursorEffect.GetComponent<Image>().color;
            float onAlphaValue = 0.1f;
            float offAlphaValue = 0f;

            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                foreach (var hit in hits)
                {
                    foreach (var boxCollider2D in boxCollider2DList)
                    {
                        if (hit.transform == boxCollider2D.Key.transform)
                        {
                            onCursorEffect.transform.position = boxCollider2D.Key.transform.position;
                            color.a = onAlphaValue;
                            onCursorEffect.GetComponent<Image>().color = color;
                            return;
                        }
                    }
                }

                color.a = offAlphaValue;
                onCursorEffect.GetComponent<Image>().color = color;
            }
        }

        private void Update()
        {
            CheckScroll();
            CheckClick();
            OnCursorEffect();
        }
    }
}
