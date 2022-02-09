using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace StoryBoardEditor
{
    public class StoryBoardEditorLine
    {
        public StoryBoardNode node01;
        public StoryBoardNode node02;
        public GameObject lineObject;
        public LineRenderer lineRenderer;
    }

    public class StoryBoardEditorTempLine
    {
        public StoryBoardNode node;
        public GameObject lineObject;
        public LineRenderer lineRenderer;
        public LineEdge edge;
    }

    public enum LineEdge
    {
        Input,
        Output
    }

    public class StoryBoardEditorLineManager : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorLineManager _instance;

        public static StoryBoardEditorLineManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorLineManager>();
                if (obj == null)
                {
                    Debug.LogError("StoryBoardEditorLineManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        [SerializeField] private GameObject line;
        [SerializeField] private GameObject lineLayer;

        [SerializeField] private StoryBoardEditorTempLine tempLine;
        
        List<StoryBoardEditorLine> _lineList = new List<StoryBoardEditorLine>();
        private void MakeLine()
        {
        
        }
        
        public void DrawTempLine(StoryBoardNode node, LineEdge edge)
        {
            GameObject lineObject = Instantiate(line, lineLayer.transform);
            Vector3 pos1 = node.nodeObject.transform.Find(edge.ToString()).transform.position;
            pos1.z = 0;
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
            Vector3 pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos2.z = 0;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
            
            StoryBoardEditorTempLine newTempLine = new StoryBoardEditorTempLine();
            newTempLine.node = node;
            newTempLine.lineObject = lineObject;
            newTempLine.edge = edge;
            newTempLine.lineRenderer = lineObject.GetComponent<LineRenderer>();
            tempLine = newTempLine;
        }

        public void MovePoint2OfTempLine(Vector3 pos2)
        {
            if (tempLine == null)
            {
                Debug.LogError("TempLine is not Exist");
                return;
            }
            tempLine.lineRenderer.SetPosition(1,pos2);
        }

        public void DeleteTempLine()
        {
            Destroy(tempLine.lineObject);
            tempLine = null;
        }

        public void RequestAddLine()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

            GameObject targetNode = null;
            LineEdge targetEdge = LineEdge.Output;
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("StoryBoardEditor_Node"))
                {
                    targetNode = hit.transform.gameObject;
                }

                if (hit.transform.CompareTag("StoryBoardEditor_NodeInput"))
                {
                    targetEdge = LineEdge.Input;
                }

                if (hit.transform.CompareTag("StoryBoardEditor_NodeOutput"))
                {
                    targetEdge = LineEdge.Output;
                }
            }

            if (targetNode == null)
            {
                return;
            }

            if (targetNode != tempLine.lineObject && targetEdge != tempLine.edge)
            {
                if (targetEdge == LineEdge.Input)
                {
                    AddLine(tempLine.node,StoryBoardEditorNodeManager.GetInstance().GetNodeByName(targetNode.name));
                }
                else
                {
                    AddLine(StoryBoardEditorNodeManager.GetInstance().GetNodeByName(targetNode.name),tempLine.node);
                }
            }
        }

        public void AddLine(StoryBoardNode node01, StoryBoardNode node02)
        {
            GameObject lineObject = Instantiate(line, lineLayer.transform);
            StoryBoardEditorLine newLine = new StoryBoardEditorLine();
            newLine.node01 = node01;
            newLine.node02 = node02;
            newLine.lineObject = lineObject;
            newLine.lineRenderer = newLine.lineObject.GetComponent<LineRenderer>();
            newLine.lineRenderer.SetPosition(0,node01.nodeObject.transform.Find("Output").position);
            newLine.lineRenderer.SetPosition(1,node02.nodeObject.transform.Find("Input").position);
            _lineList.Add(newLine);
            
            node01.outputLine = newLine;
            node02.inputLine = newLine;
        }

        public void UpdateLine(GameObject nodeGameObject)
        {
            StoryBoardNode node = StoryBoardEditorNodeManager.GetInstance().GetNodeByName(nodeGameObject.name);
            if (node.inputLine != null)
            {
                node.inputLine.lineRenderer.SetPosition(1,node.nodeObject.transform.Find("Input").position);
            }

            if (node.outputLine != null)
            {
                node.outputLine.lineRenderer.SetPosition(0,node.nodeObject.transform.Find("Output").position);
            }
        }

        public void DeleteLine()
        {
            
        }

        public void MovingPoint()
        {
            
        }
        
        
    }
}