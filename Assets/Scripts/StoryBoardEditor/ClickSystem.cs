using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StoryBoardEditor.NodeInfo;
using UnityEngine;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ClickMode
    {
        UI,
        UI_EditMode,
        NodeInfo,
        NodeInput,
        NodeOutput,
        Node,
        Line,
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

        private readonly List<Action<RaycastHit2D[], RaycastHit[]>> _customClickList =
            new List<Action<RaycastHit2D[], RaycastHit[]>>();
        private bool _isClickEnable = true;

        private Action<RaycastHit2D[],RaycastHit[]> MakeClick()
        {
            Action<RaycastHit2D[],RaycastHit[]> click = delegate { };

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

        private ClickMode ClickPriority(RaycastHit2D[] hits2D, RaycastHit[] hits)
        {
            List<(string, ClickMode)> priorityList = new List<(string, ClickMode)>
            {
                ("StoryBoardEditor_UI", ClickMode.UI),
                ("StoryBoardEditor_EditModeButton", ClickMode.UI_EditMode),
                ("StoryBoardEditor_NodeInfo", ClickMode.NodeInfo),
                ("StoryBoardEditor_NodeInput", ClickMode.NodeInput),
                ("StoryBoardEditor_NodeOutput", ClickMode.NodeOutput),
                ("StoryBoardEditor_Node", ClickMode.Node),
                ("StoryBoardEditor_Line",ClickMode.Line)
            };

            foreach (var priority in priorityList)
            {
                foreach (var hit2D in hits2D)
                {
                    if (hit2D.transform.CompareTag(priority.Item1))
                    {
                        return priority.Item2;
                    }
                }

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

        public GameObject GetNodeFromClick(RaycastHit2D[] hits)
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

        public GameObject GetLineFromClick(RaycastHit[] hits)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("StoryBoardEditor_Line"))
                {
                    return hit.transform.gameObject;
                }
            }

            return null;
        }

        private void Click(RaycastHit2D[] hits2D, RaycastHit[] hits)
        {
            ClickMode mode = ClickPriority(hits2D, hits);
            
            UI_ButtonManager.GetInstance().DisableUI_Button(UI_Button.Delete);
            switch (mode)
            {
                case ClickMode.UI:
                    UI_ButtonManager.GetInstance().UI_Click(hits2D);
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;
                
                case  ClickMode.UI_EditMode:
                    UI_EditModeButton.GetInstance().Click();
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;

                case ClickMode.NodeInfo:
                    NodeInfoManager.GetInstance().Click(hits2D);
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;

                case ClickMode.NodeInput:
                    NodeManipulator.GetInstance().ClickNodePort(GetNodeFromClick(hits2D), NodeEdge.Input);
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;

                case ClickMode.NodeOutput:
                    NodeManipulator.GetInstance().ClickNodePort(GetNodeFromClick(hits2D), NodeEdge.Output);
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;

                case ClickMode.Node:
                    NodeManipulator.GetInstance().LeftClickNode(GetNodeFromClick(hits2D));
                    NodeInfoManager.GetInstance().CloseNodeInfo();
                    NodeInfoManager.GetInstance().ShowNodeInfo(NodeManipulator.GetInstance().GetSelectedNode());
                    LineManipulator.GetInstance().ClearSelectedLine();
                    break;
                
                case ClickMode.Line:
                    LineManipulator.GetInstance().ClearSelectedLine();
                    LineManipulator.GetInstance().SetSelectedLine(GetLineFromClick(hits));
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    Debug.Log(LineManipulator.GetInstance().GetSelectedLine().id);
                    break;

                case ClickMode.Null:
                    NodeManipulator.GetInstance().ClearSelectedNode();
                    LineManipulator.GetInstance().ClearSelectedLine();
                    NodeInfoManager.GetInstance().CloseNodeInfo();
                    break;
            }
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits2D = Physics2D.GetRayIntersectionAll(ray);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                Action<RaycastHit2D[], RaycastHit[]> click = MakeClick();
                click(hits2D, hits);
            }
        }

        private void Update()
        {
            CheckClick();
        }
        
        public void SubscribeCustomClick(Action<RaycastHit2D[],RaycastHit[]> click)
        {
            if (!_customClickList.Contains(click))
            {
                _customClickList.Add(click);
            }
        }

        public void UnsubscribeCustomClick(Action<RaycastHit2D[],RaycastHit[]> click)
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

