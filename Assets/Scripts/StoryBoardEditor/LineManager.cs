using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace StoryBoardEditor
{
    public class Line
    {
        public Node node01;
        public Node node02;
        public GameObject lineObject;
        public LineRenderer lineRenderer;
    }

    public class TempLine
    {
        public Node node;
        public GameObject lineObject;
        public LineRenderer lineRenderer;
        public LineEdge edge;
    }

    public enum LineEdge
    {
        Input,
        Output
    }

    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "Unity.PreferNonAllocApi")]
    
    public class LineManager : MonoBehaviour
    {
        #region Singleton

        private static LineManager _instance;

        public static LineManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LineManager>();
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
        
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject lineLayer;

        private TempLine _tempLine;
        
        private readonly List<Line> _lineList = new List<Line>();

        #region TempLine

        public void RequestDrawTempLine(Node node, LineEdge edge)
        {
            Line line = node.GetLine(edge);
            if (line != null)
            {
                switch (edge)
                {
                    case LineEdge.Output:
                        node = node.GetNextNode();
                        break;
                    case LineEdge.Input:
                        node = node.GetPrevNode();
                        break;
                }

                edge = ReverseEdge(edge);
                RemoveLine(line);
            }

            DrawTempLine(node, edge);
        }
        
        private void DrawTempLine(Node node, LineEdge edge)
        {
            GameObject lineObject = Instantiate(linePrefab, lineLayer.transform);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

            Vector3 pos1 = node.nodeObject.transform.Find(edge.ToString()).transform.position;
            pos1.z = 0;
            Vector3 pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos2.z = 0;

            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);

            TempLine newTempLine = new TempLine
            {
                node = node,
                lineObject = lineObject,
                edge = edge,
                lineRenderer = lineObject.GetComponent<LineRenderer>()
            };
            _tempLine = newTempLine;
        }

        public void MovePoint2OfTempLine(Vector3 pos2)
        {
            if (_tempLine == null)
            {
                Debug.LogError("TempLine is not Exist");
                return;
            }
            _tempLine.lineRenderer.SetPosition(1,pos2);
        }

        public void DeleteTempLine()
        {
            Destroy(_tempLine.lineObject);
            _tempLine = null;
        }

        #endregion

        #region CheckMethod

        private bool CheckLineDuplicated(Node node)
        {
            LineEdge edge = ReverseEdge(_tempLine.edge);
            if (node.GetLine(edge) != null)
            {
                Debug.Log("Line Duplicated");
                return true;
            }

            return false;
        }

        private bool CheckLineLoop(Node node)
        {
            Node targetNode = node;
            Node currentNode = node;

            int n = 0;
            while (true)
            {
                n++;
                if (n > 10)
                {
                    Debug.LogError("Infinite Loop");
                    return false;
                }
                
                currentNode = currentNode.GetNextNode();
                if (currentNode == null)
                {
                    return false;
                }

                if (currentNode == targetNode)
                {
                    Debug.Log("Line Loop");
                    return true;
                }
            }
        }

        #endregion
        
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

            Node node = NodeManager.GetInstance().GetNodeByName(targetNode.name);

            if (CheckLineDuplicated(node))
            {
                return;
            }

            if (targetNode != _tempLine.node.nodeObject && targetEdge != _tempLine.edge)
            {
                if (targetEdge == LineEdge.Input)
                {
                    AddLine(_tempLine.node,NodeManager.GetInstance().GetNodeByName(targetNode.name));
                }
                else
                {
                    AddLine(NodeManager.GetInstance().GetNodeByName(targetNode.name),_tempLine.node);
                }
            }
            
            if (CheckLineLoop(node))
            {
                RemoveLine(_lineList[_lineList.Count-1]);
            }
        }

        private void AddLine(Node node01, Node node02)
        {
            GameObject lineObject = Instantiate(linePrefab, lineLayer.transform);
            Line newLine = new Line();
            newLine.node01 = node01;
            newLine.node02 = node02;
            newLine.lineObject = lineObject;
            newLine.lineRenderer = newLine.lineObject.GetComponent<LineRenderer>();
            newLine.lineRenderer.SetPosition(0,node01.nodeObject.transform.Find("Output").position);
            newLine.lineRenderer.SetPosition(1,node02.nodeObject.transform.Find("Input").position);
            _lineList.Add(newLine);

            node01.SetLine(LineEdge.Output, newLine);
            node02.SetLine(LineEdge.Input,newLine);
            node01.SetNextNode(node02);
            node02.SetPrevNode(node01);
        }

        public void UpdateLine(Node node)
        {
            if (node.GetLine(LineEdge.Input) != null)
            {
                node.GetLine(LineEdge.Input).lineRenderer.SetPosition(1,node.nodeObject.transform.Find("Input").position);
            }

            if (node.GetLine(LineEdge.Output) != null)
            {
                node.GetLine(LineEdge.Output).lineRenderer.SetPosition(0,node.nodeObject.transform.Find("Output").position);
            }
        }

        private void RemoveLine(Line line)
        {
            Node node01 = line.node01;
            Node node02 = line.node02;
            node01.SetNextNode(null);
            node02.SetPrevNode(null);
            node01.SetLine(LineEdge.Output,null);
            node02.SetLine(LineEdge.Input,null);
            _lineList.Remove(line);
            Destroy(line.lineObject);
        }

        private LineEdge ReverseEdge(LineEdge edge)
        {
            switch (edge)
            {
                case LineEdge.Input:
                    edge = LineEdge.Output;
                    break;
                
                case LineEdge.Output:
                    edge = LineEdge.Input;
                    break;
            }

            return edge;
        }
    }
}