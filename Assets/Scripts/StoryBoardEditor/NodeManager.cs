using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace StoryBoardEditor
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
        
        private Dictionary<string,Node> _nodeList = new Dictionary<string, Node>();
        private Dictionary<GameObject,Node> _nodeGameObjectList = new Dictionary<GameObject, Node>();
        [SerializeField] private GameObject dialogueNodePrefab;
        [SerializeField] private GameObject selectionNodePrefab;
        [SerializeField] private GameObject selectionTextNodePrefab;
        [SerializeField] private GameObject gainClueNodePrefab;
        [SerializeField] public int nodeIdCount = 0;

        public List<GameObject> GetAllNodeGameObject()
        {
            return _nodeGameObjectList.Keys.ToList();
        }

        public List<Node> GetAllNode()
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
                
                case NodeType.GainClue:
                    nodeGameObject = Instantiate(gainClueNodePrefab,position,quaternion.identity ,this.transform);
                    break;
                
                default:
                    nodeGameObject = Instantiate(dialogueNodePrefab,position,quaternion.identity ,this.transform);
                    break;
            }
            
            nodeGameObject.name = SetNodeId();
            Node node = new Node {type = type, id = nodeGameObject.name, gameObject = nodeGameObject};
            node.input = node.gameObject.transform.Find("Input").gameObject;
            node.output = node.gameObject.transform.Find("Output").gameObject;
            
            _nodeList.Add(nodeGameObject.name, node);
            _nodeGameObjectList.Add(nodeGameObject,node);
        }

        public void MakeNodeFromLoadData(NodeData nodeData)
        {
            Vector3 position = new Vector3(nodeData.x, nodeData.y, 0);
            GameObject nodeGameObject = Instantiate(dialogueNodePrefab,position,quaternion.identity ,this.transform);
            nodeGameObject.name = nodeData.nodeId;

            Node newNode = new Node {id = nodeData.nodeId, gameObject = nodeGameObject};
            newNode.input = newNode.gameObject.transform.Find("Input").gameObject;
            newNode.output = newNode.gameObject.transform.Find("Output").gameObject;
            
            _nodeList.Add(newNode.id,newNode);
            _nodeGameObjectList.Add(nodeGameObject, newNode);
        }

        public void SetNodeFromLoadData(NodeData nodeData)
        {
            Node node = _nodeList[nodeData.nodeId];
            foreach (var inputLine in nodeData.inputLineIdList)
            {
                node.inputLineList.Add(LineManager.GetInstance().GetLine(inputLine));
            }

            foreach (var outputLine in nodeData.outputLineIdList)
            {
                node.inputLineList.Add(LineManager.GetInstance().GetLine(outputLine));
            }
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

        public Node GetNodeByGameObject(GameObject nodeGameObject)
        {
            return _nodeGameObjectList[nodeGameObject];
        }

        public Node GetNodeByName(string nameValue)
        {
            return _nodeList[nameValue];
        }

        public void RemoveNode(Node node)
        {
            List<Line> connectedLineList = new List<Line>();
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
