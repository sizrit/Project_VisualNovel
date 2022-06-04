using System.Collections.Generic;
using System.Linq;
using StoryBoardEditor.Line;
using Unity.Mathematics;
using UnityEngine;

namespace StoryBoardEditor.Node
{
    public class NodeManager : MonoBehaviour
    {
        #region Singleton

        private static NodeManager _instance;

        public static NodeManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<NodeManager>();
                if (obj == null)
                {
                    Debug.LogError("NodeManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }
        
        #endregion
        
        private readonly Dictionary<string,StoryBoardEditor.Node.Node> _nodeList = new Dictionary<string, StoryBoardEditor.Node.Node>();
        private readonly Dictionary<GameObject,StoryBoardEditor.Node.Node> _nodeGameObjectList = new Dictionary<GameObject, StoryBoardEditor.Node.Node>();
        [SerializeField] private GameObject dialogueNodePrefab;
        [SerializeField] private GameObject selectionNodePrefab;
        [SerializeField] private GameObject selectionTextNodePrefab;
        [SerializeField] private GameObject getClueNodePrefab;
        [SerializeField] private GameObject getItemNodePrefab;
        [SerializeField] private GameObject eventNodePrefab;
        [SerializeField] public int nodeIdCount = 0;

        public List<GameObject> GetAllNodeGameObject()
        {
            return _nodeGameObjectList.Keys.ToList();
        }

        public List<StoryBoardEditor.Node.Node> GetAllNode()
        {
            return _nodeList.Values.ToList();
        }
        
        public void AddNode(Vector3 position, NodeType type)
        {
            GameObject nodeGameObject = null;
            switch (type)
            {
                case NodeType.Dialogue:
                    nodeGameObject = Instantiate(dialogueNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                case  NodeType.Selection:
                    nodeGameObject = Instantiate(selectionNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                case NodeType.SelectionText:
                    nodeGameObject = Instantiate(selectionTextNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                case NodeType.GetClue:
                    nodeGameObject = Instantiate(getClueNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                case NodeType.GetItem:
                    nodeGameObject = Instantiate(getItemNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                case  NodeType.Event:
                    nodeGameObject = Instantiate(eventNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                default:
                    nodeGameObject = Instantiate(dialogueNodePrefab,position,quaternion.identity ,this.transform);
                    break;
            }
            
            nodeGameObject.name = SetNodeId();
            StoryBoardEditor.Node.Node node = new StoryBoardEditor.Node.Node {type = type, id = nodeGameObject.name, gameObject = nodeGameObject};
            node.input = node.gameObject.transform.Find("Input").gameObject;
            node.output = node.gameObject.transform.Find("Output").gameObject;
            
            _nodeList.Add(nodeGameObject.name, node);
            _nodeGameObjectList.Add(nodeGameObject,node);
            
            NodeVisualizeSettingManager.GetInstance().SetNode(node);
        }

        public void AddToList(StoryBoardEditor.Node.Node node)
        {
            _nodeList.Add(node.id, node);
            _nodeGameObjectList.Add(node.gameObject,node);
        }
        
        public void ClearAllNode()
        {
            foreach (var node in _nodeList)
            {
                Destroy(node.Value.gameObject);
            }
            _nodeList.Clear();
            _nodeGameObjectList.Clear();
        }

        private string SetNodeId()
        {
            return "N"+nodeIdCount++.ToString("D4");;
        }

        public StoryBoardEditor.Node.Node GetNodeByGameObject(GameObject nodeGameObject)
        {
            return _nodeGameObjectList[nodeGameObject];
        }

        public StoryBoardEditor.Node.Node GetNodeByName(string nameValue)
        {
            return _nodeList[nameValue];
        }

        public void RemoveNode(StoryBoardEditor.Node.Node node)
        {
            NodeManipulator.GetInstance().ClearSelectedNode();
            
            List<Line.Line> connectedLineList = new List<Line.Line>();
            connectedLineList.AddRange(node.inputLineList);
            connectedLineList.AddRange(node.outputLineList);

            foreach (var line in connectedLineList)
            {
                LineManager.GetInstance().RemoveLine(line);
            }

            Destroy(node.gameObject);
            _nodeList.Remove(node.id);
            _nodeGameObjectList.Remove(node.gameObject);
        }
    }
}
