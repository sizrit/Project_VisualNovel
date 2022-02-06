using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    public class StoryBoardEditorNodeInfoScrollSelectionManager : MonoBehaviour
    {
        private enum ScrollDirection
        {
            Up,
            Down
        }
        [SerializeField] private GameObject contentText;
        [SerializeField] private GameObject text;
        [SerializeField] private GameObject boxCollider01;
        [SerializeField] private GameObject boxCollider02;
        
        [SerializeField] private int currentTopIndex = 0;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                foreach (var hit in hits)
                {
                    if (hit.transform.gameObject == boxCollider01)
                    {
                        Debug.Log("sss");
                    }
                    if (hit.transform.gameObject == boxCollider02)
                    {
                        Debug.Log("sss2");
                    }
                }
            }
        }


        private void ScrollContent(ScrollDirection direction)
        {
            switch (direction)
            {
                case ScrollDirection.Up:
                    if (currentTopIndex != 0)
                    {
                        
                    }
                    break;
                case ScrollDirection.Down:
                    if (currentTopIndex != 0)
                    {
                        
                    }
                    break;
            }
        }

        public void CheckScroll()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                ScrollContent(ScrollDirection.Up);
            }
            
            if (Input.mouseScrollDelta.y < 0)
            {
                ScrollContent(ScrollDirection.Down);
            }
        }

        public void CheckClick()
        {
            
        }
        
        public void SetContentText(List<string> contentList)
        {
            string toString = "";
            foreach (var content in contentList)
            {
                toString += content;
            }
            contentText.GetComponent<Text>().text = toString;
        }
        
        private void SetText(string text)
        {
            
        }

        public void SetBoxCollider()
        {
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }


    }
}
