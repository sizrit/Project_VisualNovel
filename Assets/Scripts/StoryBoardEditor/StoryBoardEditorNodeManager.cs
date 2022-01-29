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
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private int nodeIdCount = 0;
        
        public void AddNode()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            GameObject node = Instantiate(nodePrefab,position,quaternion.identity ,this.transform);
            node.name = SetNodeId();
            _nodeList.Add(node.name, new StoryBoardNode(node.name,node));
        }

        private string SetNodeId()
        {
            return "N"+nodeIdCount++.ToString("D4");;
        }

        public void SetPosition(Vector3 position)
        {
            
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
