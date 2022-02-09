using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum ClickMode
    {
        UI,
        NodeInput,
        NodeOutput,
        Node,
        Null
    }

    public class StoryBoardEditorClickSystem : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorClickSystem _instance;

        public static StoryBoardEditorClickSystem GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorClickSystem>();
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

        [SerializeField] private GameObject currentSelectedNode = null;
        private Vector3 _prevPosition = Vector3.zero;
        private Action _checkFunc = delegate { };

        public GameObject GetCurrentSelectedNode()
        {
            return currentSelectedNode;
        }

        private ClickMode ClickPriority(RaycastHit2D[] hits)
        {
            List<(string, ClickMode)> priorityList = new List<(string, ClickMode)>
            {
                ("StoryBoardEditor_UI", ClickMode.UI),
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
                        StoryBoardEditorNodeInfoManager.GetInstance().CheckClick(hits);
                        break;
                    
                    case  ClickMode.NodeInput:
                        currentSelectedNode = GetNodeFromClick(hits);
                        StoryBoardEditorLineManager.GetInstance().DrawTempLine(StoryBoardEditorNodeManager.GetInstance()
                            .GetNodeByName(currentSelectedNode.name),LineEdge.Input);
                        _checkFunc = DragTempLine;
                        break;
                    
                    case  ClickMode.NodeOutput:
                        currentSelectedNode = GetNodeFromClick(hits);
                        StoryBoardEditorLineManager.GetInstance().DrawTempLine(StoryBoardEditorNodeManager.GetInstance()
                            .GetNodeByName(currentSelectedNode.name),LineEdge.Output);
                        _checkFunc = DragTempLine;
                        break;
                    
                    case ClickMode.Node:
                        currentSelectedNode = GetNodeFromClick(hits);
                        _prevPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        _checkFunc = DragNode;
                        StoryBoardEditorNodeInfoManager.GetInstance().EnableNodeInfo(StoryBoardEditorNodeManager
                            .GetInstance().GetNodeByName(currentSelectedNode.name));
                        break;

                    case ClickMode.Null:
                        currentSelectedNode = null;
                        StoryBoardEditorNodeInfoManager.GetInstance().DisableNodeInfo();
                        break;
                }
            }
        }

        private void DragTempLine()
        {
            Debug.Log("draw Temp Line");
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            StoryBoardEditorLineManager.GetInstance().MovePoint2OfTempLine(pos);
            
            if (Input.GetMouseButtonUp(0))
            {
                StoryBoardEditorLineManager.GetInstance().RequestAddLine();
                StoryBoardEditorLineManager.GetInstance().DeleteTempLine();
                _checkFunc = delegate { };
            }
        }

        private void DragNode()
        {
            Debug.Log("drag");
            if (Input.GetMouseButton(0) && currentSelectedNode != null)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPosition.z = 0;
                if (_prevPosition != currentPosition)
                {
                    Vector3 delta = currentPosition - _prevPosition;
                    currentSelectedNode.transform.position += delta;
                    _prevPosition = currentPosition;
                    
                    StoryBoardEditorLineManager.GetInstance().UpdateLine(currentSelectedNode);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _checkFunc = delegate { };
                currentSelectedNode.transform.position = StoryBoardEditorGridSystem.GetInstance()
                    .SetPositionToGrid(currentSelectedNode.transform.position);
                StoryBoardEditorLineManager.GetInstance().UpdateLine(currentSelectedNode);
            }
        }

        public void DeleteCheck()
        {
            if (currentSelectedNode != null)
            {
                StoryBoardEditorNodeManager.GetInstance().DeleteNode(currentSelectedNode);
                currentSelectedNode = null;
            }
        }

        private void Update()
        {
            CheckClick();
            _checkFunc();
        }
    }
}

