using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NodeManipulator : MonoBehaviour
    {
        #region Singleton

        private static NodeManipulator _instance;

        public static NodeManipulator GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<NodeManipulator>();
                if (obj == null)
                {
                    Debug.LogError("NodeManipulator Script is not available!");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }
        
        #endregion

        [SerializeField] private Node selectedNode;

        private Vector3 _prevPosition = Vector3.zero;
        private Vector3 _startPosition = Vector3.zero;
        
        Action _updateFunc = delegate { };

        public void LeftClickNode(GameObject node)
        {
            SetSelectedNode(NodeManager.GetInstance().GetNodeByGameObject(node));
            _prevPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startPosition = selectedNode.gameObject.transform.position;
            _updateFunc = DragNode;
        }

        private void DragNode()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPosition.z = 0;
                if (_prevPosition != currentPosition)
                {
                    Vector3 delta = currentPosition - _prevPosition;
                    selectedNode.gameObject.transform.position += delta;
                    _prevPosition = currentPosition;
                    
                    LineManager.GetInstance().UpdateLine(selectedNode);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _updateFunc = delegate { };
                NodeCollisionManager.GetInstance().CheckCollision(selectedNode.gameObject,_startPosition);
                selectedNode.gameObject.transform.position = GridSystem.GetInstance()
                    .SetPositionToGrid(selectedNode.gameObject.transform.position);
                LineManager.GetInstance().UpdateLine(selectedNode);
            }
        }

        public void ClickNodePort(GameObject node, NodeEdge edge)
        {
            selectedNode = NodeManager.GetInstance().GetNodeByGameObject(node);
            if (TempLineManager.GetInstance().RequestDrawTempLine(selectedNode, edge))
            {
                _updateFunc = DragTempLine;
            }
        }
        
        private void DragTempLine()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            TempLineManager.GetInstance().MovePoint2OfTempLine(pos);
            
            if (Input.GetMouseButtonUp(0))
            {
                LineManager.GetInstance().RequestAddLine();
                TempLineManager.GetInstance().DeleteTempLine();
                _updateFunc = delegate { };
            }
        }

        public Node GetSelectedNode()
        {
            return selectedNode;
        }

        public void SetSelectedNode(Node node)
        {
            selectedNode = node;         
            UI_ButtonManager.GetInstance().EnableUI_Button(UI_Button.Delete);
        }
        
        public void ClearSelectedNode()
        {
            selectedNode = null;
            UI_ButtonManager.GetInstance().DisableUI_Button(UI_Button.Delete);
        }

        private void Update()
        {
            _updateFunc();
        }
    }
}
