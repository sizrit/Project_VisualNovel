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
        public string id;
        public Node node01;
        public Node node02;
        public GameObject gameObject;
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

        [SerializeField] public int lineCount = 0;

        #region TempLine

        public void RequestDrawTempLine(Node node, LineEdge edge)
        {
            Line line = node.GetLine(edge);
            if (line != null)
            {
                switch (edge)
                {
                    case LineEdge.Output:
                        node = node.nextNode;
                        break;
                    case LineEdge.Input:
                        node = node.prevNode;
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

            Vector3 pos1 = node.gameObject.transform.Find(edge.ToString()).transform.position;
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
                
                currentNode = currentNode.nextNode;
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

        private string SetLineId()
        {
            return "L"+lineCount++.ToString("D4");;
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

            Node node = NodeManager.GetInstance().GetNodeByName(targetNode.name);

            if (CheckLineDuplicated(node))
            {
                return;
            }

            if (targetNode != _tempLine.node.gameObject && targetEdge != _tempLine.edge)
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
            Line newLine = new Line {id = SetLineId(), node01 = node01, node02 = node02, gameObject = lineObject};
            newLine.lineRenderer = newLine.gameObject.GetComponent<LineRenderer>();
            newLine.lineRenderer.SetPosition(0, node01.output.transform.position);
            newLine.lineRenderer.SetPosition(1, node02.input.transform.position);
            _lineList.Add(newLine);

            node01.outputLine = newLine;
            node02.inputLine = newLine;
            node01.nextNode = node02;
            node02.prevNode = node01;
        }

        public void UpdateLine(Node node)
        {
            node.inputLine?.lineRenderer.SetPosition(1,node.input.transform.position);

            node.outputLine?.lineRenderer.SetPosition(0,node.output.transform.position);
        }

        private void RemoveLine(Line line)
        {
            Node node01 = line.node01;
            Node node02 = line.node02;
            node01.nextNode =null;
            node02.prevNode =null;
            node01.outputLine =null;
            node02.inputLine =null;
            _lineList.Remove(line);
            Destroy(line.gameObject);
        }

        public void MakeLineFromLoadData(LineData lineData)
        {
            Line newLine = new Line();
            newLine.id = lineData.lineId;
            newLine.gameObject = Instantiate(linePrefab,lineLayer.transform);
            newLine.lineRenderer = newLine.gameObject.GetComponent<LineRenderer>();
            _lineList.Add(newLine);
        }

        public void SetLineFromLoadData(LineData lineData)
        {
            Line targetLine = new Line();
            foreach (var line in _lineList)
            {
                if (line.id == lineData.lineId)
                {
                    targetLine = line;
                }
            }

            targetLine.node01 = NodeManager.GetInstance().GetNodeByName(lineData.node01Id);
            targetLine.node02 = NodeManager.GetInstance().GetNodeByName(lineData.node02Id);
            
            targetLine.lineRenderer.SetPosition(0,targetLine.node01.output.transform.position);
            targetLine.lineRenderer.SetPosition(1,targetLine.node02.input.transform.position);
        }

        public void ClearAllLine()
        {
            foreach (var line in _lineList)
            {
                Destroy(line.gameObject);
            }
            _lineList.Clear();
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

        public Line GetLine(string lineId)
        {
            foreach (var line in _lineList)
            {
                if (lineId == line.id)
                {
                    return line;
                }
            }

            return null;
        }

        public List<Line> GetAllLine()
        {
            return _lineList;
        }
    }
}