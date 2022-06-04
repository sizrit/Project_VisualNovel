using System.Diagnostics.CodeAnalysis;
using StoryBoardEditor.Node;
using UnityEngine;

namespace StoryBoardEditor.Line
{
    public class TempLine
    {
        public Node.Node node;
        public GameObject lineObject;
        public LineRenderer lineRenderer;
        public NodeEdge startEdge;
    }
    
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class TempLineManager : MonoBehaviour
    {
        #region Singleton

        private static TempLineManager _instance;

        public static TempLineManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<TempLineManager>();
                if (obj == null)
                {
                    Debug.LogError("TempLineManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        private TempLine _tempLine;
        
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject lineLayer;
        
        public bool RequestDrawTempLine(Node.Node node, NodeEdge edge)
        {
            // NodeType 이 selection 이 아닌데 OutPut 을 여러개 만들려고 하는경우
            if (node.type != NodeType.Selection && node.outputLineList.Count != 0 && edge == NodeEdge.Output)
            {
                Debug.Log(" NodeType : "+node.type+" can't make more than 1 OutputLines");
                _tempLine = null;
                LineManipulator.GetInstance().ClearSelectedLine();
                return false;
            }
            
            StoryBoardEditor.Line.Line selectedLine = LineManipulator.GetInstance().GetSelectedLine();

            if (selectedLine != null)
            {
                switch (edge)
                {
                    case NodeEdge.Input:
                        if (node == selectedLine.node02)
                        {
                            LineManager.GetInstance().RemoveLine(selectedLine);
                            DrawTempLine(selectedLine.node01, NodeEdge.Output);
                        }
                        break;

                    case NodeEdge.Output:
                        if (node == selectedLine.node01)
                        {
                            LineManager.GetInstance().RemoveLine(selectedLine);
                            DrawTempLine(selectedLine.node02, NodeEdge.Input);
                        }
                        break;
                }
            }
            else
            {
                DrawTempLine(node, edge);
            }
            
            return true;
        }

        private void DrawTempLine(Node.Node node, NodeEdge edge)
        {
            GameObject lineObject = Instantiate(linePrefab, lineLayer.transform);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

            Vector3 pos1 = Vector3.zero;
            switch (edge)
            {
                case NodeEdge.Output:
                    pos1 = node.output.transform.position;
                    break;
                case  NodeEdge.Input:
                    pos1 = node.input.transform.position;
                    break;
            }
            pos1.z = 0;
            Vector3 pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos2.z = 0;

            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);

            TempLine newTempLine = new TempLine
            {
                node = node,
                lineObject = lineObject,
                startEdge = edge,
                lineRenderer = lineObject.GetComponent<LineRenderer>()
            };
            _tempLine = newTempLine;
            
            TempLineSelectEffectManager.GetInstance().ShowEffect(_tempLine);
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
            if (_tempLine != null)
            {
                TempLineSelectEffectManager.GetInstance().RemoveEffect(_tempLine);
                Destroy(_tempLine.lineObject);
            }
            _tempLine = null;
            
        }

        public TempLine GetTempLine()
        {
            return _tempLine;
        }
    }
}
