using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum ClickMode
    {
        UI,
        NodeInfo,
        NodeInput,
        NodeOutput,
        Node,
        Null
    }

    public class ClickSystem : MonoBehaviour
    {
        #region Singleton

        private static ClickSystem _instance;

        public static ClickSystem GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<ClickSystem>();
                if (obj == null)
                {
                    Debug.LogError("StoryBoardEditorClickSystem Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        private Action _checkFunc = delegate { };

        private ClickMode ClickPriority(RaycastHit2D[] hits)
        {
            List<(string, ClickMode)> priorityList = new List<(string, ClickMode)>
            {
                ("StoryBoardEditor_UI", ClickMode.UI),
                ("StoryBoardEditor_NodeInfo",ClickMode.NodeInfo),
                ("StoryBoardEditor_NodeInput", ClickMode.NodeInput),
                ("StoryBoardEditor_NodeOutput", ClickMode.NodeOutput),
                ("StoryBoardEditor_Node", ClickMode.Node)
            };

            foreach (var priority in priorityList)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.CompareTag(priority.Item1))
                    {
                        return priority.Item2;
                    }
                }
            }

            return ClickMode.Null;
        }

        private GameObject GetNodeFromClick(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("StoryBoardEditor_Node"))
                {
                    return hit.transform.gameObject;
                }
            }

            return null;
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                ClickMode mode = ClickPriority(hits);

                switch (mode)
                {
                    case ClickMode.UI:
                        UI_EditButton.GetInstance().Click();
                        break;
                    
                    case ClickMode.NodeInfo:
                        NodeInfoManager.GetInstance().CheckClick(hits);
                        break;
                    
                    case  ClickMode.NodeInput:
                        NodeManipulator.GetInstance().ClickNodeInput(GetNodeFromClick(hits));
                        break;
                    
                    case  ClickMode.NodeOutput:
                        NodeManipulator.GetInstance().ClickNodeOutput(GetNodeFromClick(hits));
                        break;
                    
                    case ClickMode.Node:
                        NodeManipulator.GetInstance().LeftClickNode(GetNodeFromClick(hits));
                        NodeInfoManager.GetInstance().EnableNodeInfo(NodeManipulator.GetInstance().GetSelectedNode());
                        break;

                    case ClickMode.Null:
                        NodeInfoManager.GetInstance().DisableNodeInfo();
                        break;
                }
            }
        }

        private void Update()
        {
            CheckClick();
            _checkFunc();
        }
    }
}

