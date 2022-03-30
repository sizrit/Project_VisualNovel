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

    public enum NodeEdge
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
                    Debug.LogError("LineManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject lineLayer;

        private readonly List<Line> _lineList = new List<Line>();

        [SerializeField] public int lineCount = 0;
        
        private string SetLineId()
        {
            return "L"+lineCount++.ToString("D4");;
        }
        
        public void RequestAddLine()
        {
            TempLine tempLine = TempLineManager.GetInstance().GetTempLine();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

            GameObject targetNode = ClickSystem.GetInstance().GetNodeFromClick(hits);
            
            if (targetNode == null)
            {
                LineManipulator.GetInstance().ClearSelectedLine();
                return;
            }
            
            Node node = NodeManager.GetInstance().GetNodeByName(targetNode.name);
            NodeEdge startEdge = tempLine.startEdge;
            
            // Check Duplicated
            switch (startEdge)
            {
                case NodeEdge.Output:
                    foreach (var line in node.outputLineList)
                    {
                        if (line.node01 == tempLine.node && line.node02 == node)
                        {
                            LineManipulator.GetInstance().ClearSelectedLine();
                            return;
                        }
                    }
                    break;
                
                case NodeEdge.Input:
                    foreach (var line in node.inputLineList)
                    {
                        if (line.node02 == tempLine.node && line.node01 == node)
                        {
                            LineManipulator.GetInstance().ClearSelectedLine();
                            return;
                        }
                    }
                    break;
            }

            // 같은 Node 의 input output 연결 방지
            if (node == tempLine.node)
            {
                LineManipulator.GetInstance().ClearSelectedLine();
                return;
            }

            switch (startEdge)
            {
                case NodeEdge.Output:
                    AddLine(tempLine.node,node);
                    break;
                
                case NodeEdge.Input:
                    AddLine(node,tempLine.node);
                    break;
            }
        }

        private void AddLine(Node node01, Node node02)
        {
            GameObject lineObject = Instantiate(linePrefab, lineLayer.transform);
            Line newLine = new Line {id = SetLineId(), node01 = node01, node02 = node02, gameObject = lineObject};
            newLine.lineRenderer = newLine.gameObject.GetComponent<LineRenderer>();
            newLine.lineRenderer.SetPosition(0, node01.output.transform.position);
            newLine.lineRenderer.SetPosition(1, node02.input.transform.position);

            LineManipulator.GetInstance().SetMeshCollider(newLine);
            
            lineObject.name = newLine.id;
            _lineList.Add(newLine);

            node01.outputLineList.Add(newLine);
            node02.inputLineList.Add(newLine);
        }

        public void UpdateLine(Node node)
        {
            foreach (var outputLine in node.outputLineList)
            {
                outputLine.lineRenderer.SetPosition(0,node.output.transform.position);
            }
            
            foreach (var inputLine in node.inputLineList)
            {
                inputLine.lineRenderer.SetPosition(1,node.input.transform.position);
            }
        }

        public void RemoveLine(Line line)
        {
            Node node01 = line.node01;
            Node node02 = line.node02;
            node01.outputLineList.Remove(line);
            node02.inputLineList.Remove(line);
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
        
        public Line GetLine(GameObject lineGameObject)
        {
            foreach (var line in _lineList)
            {
                if (lineGameObject == line.gameObject)
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