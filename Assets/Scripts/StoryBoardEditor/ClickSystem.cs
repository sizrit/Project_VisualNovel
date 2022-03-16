using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "Unity.PreferNonAllocApi")]
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

        readonly List<Action<RaycastHit2D[]>> _customClickList = new List<Action<RaycastHit2D[]>>();
        private bool _isClickEnable = true;

        private Action<RaycastHit2D[]> MakeClick()
        {
            Action<RaycastHit2D[]> click = delegate { };

            if (_isClickEnable)
            {
                click = Click;
            }

            foreach (var customClick in _customClickList)
            {
                click += customClick;
            }

            return click;
        }

        private ClickMode ClickPriority(RaycastHit2D[] hits)
        {
            List<(string, ClickMode)> priorityList = new List<(string, ClickMode)>
            {
                ("StoryBoardEditor_UI", ClickMode.UI),
                ("StoryBoardEditor_NodeInfo", ClickMode.NodeInfo),
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

                if (hit.transform.CompareTag("StoryBoardEditor_NodeInput") ||
                    hit.transform.CompareTag("StoryBoardEditor_NodeOutput"))
                {
                    return hit.transform.parent.gameObject;
                }
            }

            return null;
        }

        private void Click(RaycastHit2D[] hits)
        {
            ClickMode mode = ClickPriority(hits);
            
            UI_ButtonManager.GetInstance().DisableUI_Button(UI_Button.Delete);
            switch (mode)
            {
                case ClickMode.UI:
                    UI_ButtonManager.GetInstance().UI_Click(hits);
                    //UI_EditButton.GetInstance().Click();
                    break;

                case ClickMode.NodeInfo:
                    NodeInfoManager.GetInstance().CheckClick(hits);
                    break;

                case ClickMode.NodeInput:
                    NodeManipulator.GetInstance().ClickNodeInput(GetNodeFromClick(hits));
                    break;

                case ClickMode.NodeOutput:
                    NodeManipulator.GetInstance().ClickNodeOutput(GetNodeFromClick(hits));
                    break;

                case ClickMode.Node:
                    NodeManipulator.GetInstance().LeftClickNode(GetNodeFromClick(hits));
                    NodeInfoManager.GetInstance().EnableNodeInfo(NodeManipulator.GetInstance().GetSelectedNode());
                    UI_ButtonManager.GetInstance().EnableUI_Button(UI_Button.Delete);
                    break;

                case ClickMode.Null:
                    NodeInfoManager.GetInstance().DisableNodeInfo();
                    break;
            }
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                Action<RaycastHit2D[]> click = MakeClick();
                click(hits);
            }
        }

        private void Update()
        {
            CheckClick();
        }
        
        public void SubscribeCustomClick(Action<RaycastHit2D[]> click)
        {
            if (!_customClickList.Contains(click))
            {
                _customClickList.Add(click);
            }
        }

        public void UnsubscribeCustomClick(Action<RaycastHit2D[]> click)
        {
            if (_customClickList.Contains(click))
            {
                _customClickList.Remove(click);
            }
        }

        public void EnableClick()
        {
            _isClickEnable = true;
        }

        public void DisableClick()
        {
            _isClickEnable = false;
        }

    }
}

