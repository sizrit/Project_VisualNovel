using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    public class NodeInfoManager : MonoBehaviour
    {
        #region Singleton

        private static NodeInfoManager _instance;

        public static NodeInfoManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<NodeInfoManager>();
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

        [SerializeField] private GameObject dialogueNodeInfoPrefab;
        [SerializeField] private GameObject selectionInfoPrefab;
        [SerializeField] private GameObject selectionTextInfoPrefab;
        [SerializeField] private GameObject getClueNodeInfoPrefab;
        [SerializeField] private GameObject getItemNodeInfoPrefab;
        [SerializeField] private GameObject eventNodeInfoPrefab;
        [SerializeField] private GameObject nodeInfoGameObject;

        public void ShowNodeInfo(Node node)
        {
            switch (node.type)
            {
                case NodeType.Dialogue:
                    nodeInfoGameObject = Instantiate(dialogueNodeInfoPrefab, this.transform);
                    break;
                
                case NodeType.Selection:
                    nodeInfoGameObject = Instantiate(selectionInfoPrefab, this.transform);
                    break;
                
                case NodeType.SelectionText:
                    nodeInfoGameObject = Instantiate(selectionTextInfoPrefab, this.transform);
                    break;
                
                case NodeType.GetClue:
                    nodeInfoGameObject = Instantiate(getClueNodeInfoPrefab, this.transform);
                    break;
                
                case NodeType.GetItem:
                    nodeInfoGameObject = Instantiate(getItemNodeInfoPrefab, this.transform);
                    break;
                
                case NodeType.Event:
                    nodeInfoGameObject = Instantiate(eventNodeInfoPrefab, this.transform);
                    break;
            }
            
            nodeInfoGameObject.GetComponent<INodeInfo>().SetNodeInfo(node);
        }

        public void CloseNodeInfo()
        {
            Destroy(nodeInfoGameObject);
            nodeInfoGameObject = null;
        }

        public void Click(RaycastHit2D[] hits)
        {
            nodeInfoGameObject.GetComponent<INodeInfo>().Click(hits);
        }
    }
}

