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
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private int nodeIdCount = 0;

        public List<GameObject> GetAllNodeGameObject()
        {
            return _nodeGameObjectList.Keys.ToList();
        }
        
        public void AddNode()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            GameObject nodeGameObject = Instantiate(nodePrefab,position,quaternion.identity ,this.transform);
            nodeGameObject.name = SetNodeId();
            Node node = new Node(nodeGameObject.name, nodeGameObject);
            _nodeList.Add(nodeGameObject.name, node);
            _nodeGameObjectList.Add(nodeGameObject,node);
        }

        private string SetNodeId()
        {
            return "N"+nodeIdCount++.ToString("D4");;
        }

        public void SetPosition(Vector3 position)
        {
            
        }

        public Node GetNodeByGameObject(GameObject nodeGameObject)
        {
            return _nodeGameObjectList[nodeGameObject];
        }

        public Node GetNodeByName(string nameValue)
        {
            return _nodeList[nameValue];
        }

        public void DeleteNode(GameObject nodeGameObject)
        {
            foreach (var storyBoardNode in _nodeList.Values.ToList())
            {
                if (storyBoardNode.nodeObject == nodeGameObject)
                {
                    Destroy(nodeGameObject);
                    _nodeList.Remove(storyBoardNode.nodeId);
                }
            }
        }
    }
}
