using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorNodeManager : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorNodeManager _instance;

        public static StoryBoardEditorNodeManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorNodeManager>();
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
        
        private Dictionary<string,StoryBoardNode> _nodeList = new Dictionary<string, StoryBoardNode>();
        private Dictionary<GameObject,StoryBoardNode> _nodeGameObjectList = new Dictionary<GameObject, StoryBoardNode>();
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private int nodeIdCount = 0;
        
        public void AddNode()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            GameObject nodeGameObject = Instantiate(nodePrefab,position,quaternion.identity ,this.transform);
            nodeGameObject.name = SetNodeId();
            StoryBoardNode node = new StoryBoardNode(nodeGameObject.name, nodeGameObject);
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

        public StoryBoardNode GetNodeByGameObject(GameObject nodeGameObject)
        {
            return _nodeGameObjectList[nodeGameObject];
        }

        public StoryBoardNode GetNodeByName(string nameValue)
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
