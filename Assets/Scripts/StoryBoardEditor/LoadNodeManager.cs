using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using StoryBoardSystem;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace StoryBoardEditor
{
    public class LoadNodeManager : MonoBehaviour
    {
        #region Singleton

        private static LoadNodeManager _instance;

        public static LoadNodeManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LoadNodeManager>();
                if (obj == null)
                {
                    Debug.LogError("LoadNodeManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion

        [SerializeField] private GameObject dialogueNodePrefab;
        [SerializeField] private GameObject selectionNodePrefab;
        [SerializeField] private GameObject selectionTextNodePrefab;
        [SerializeField] private GameObject getClueNodePrefab;
        [SerializeField] private GameObject getItemNodePrefab;
        [SerializeField] private GameObject eventNodePrefab;
        
        
        public T StringToEnum<T>(string stringData)
        {
            List<T> enumValueList = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            foreach (var enumValue in enumValueList)
            {
                if (enumValue.ToString() == stringData)
                {
                    return enumValue;
                }
            }

            return enumValueList[0];
        }


        public void MakeNodeFromLoadData(NodeData nodeData)
        {
            Node newNode = new Node();

            newNode.id = nodeData.nodeId;

            newNode.type = StringToEnum<NodeType>(nodeData.type);

            GameObject nodeGameObject;

            Vector3 position = new Vector3(nodeData.x, nodeData.y, 0);

            switch (newNode.type)
            {
                case NodeType.Dialogue:
                    nodeGameObject = Instantiate(dialogueNodePrefab, position, quaternion.identity, this.transform);
                    break;

                case NodeType.Selection:
                    nodeGameObject = Instantiate(selectionNodePrefab, position, quaternion.identity, this.transform);
                    break;

                case NodeType.SelectionText:
                    nodeGameObject = Instantiate(selectionTextNodePrefab, position, quaternion.identity,
                        this.transform);
                    break;

                case NodeType.GetClue:
                    nodeGameObject = Instantiate(getClueNodePrefab, position, quaternion.identity, this.transform);
                    break;

                case NodeType.GetItem:
                    nodeGameObject = Instantiate(getItemNodePrefab, position, quaternion.identity, this.transform);
                    break;

                case NodeType.Event:
                    nodeGameObject = Instantiate(eventNodePrefab, position, quaternion.identity, this.transform);
                    break;

                default:
                    nodeGameObject = Instantiate(dialogueNodePrefab, position, quaternion.identity, this.transform);
                    break;
            }

            nodeGameObject.name = nodeData.nodeId;
            newNode.gameObject = nodeGameObject;

            newNode.isUseStaticStoryBoardId = nodeData.isUseStaticStoryBoardId;
            newNode.staticStoryBoardId = nodeData.staticStoryBoardId;

            newNode.bgId = StringToEnum<BgId>(nodeData.bgId);
            newNode.imageId = StringToEnum<ImageId>(nodeData.imageId);
            newNode.speaker = nodeData.speaker;
            newNode.dialogueText = nodeData.dialogueText;

            newNode.dialogueTextEffectId = StringToEnum<DialogueTextEffectId>(nodeData.dialogueTextEffectId);

            newNode.selectionId = nodeData.selectionId;
            newNode.selectionText = nodeData.selectionText;

            newNode.clueId = StringToEnum<Clue>(nodeData.clueId);

            newNode.itemId = StringToEnum<Item>(nodeData.itemId);

            newNode.eventId = StringToEnum<EventId>(nodeData.eventId);

            newNode.isUseTextEffect = nodeData.isUseTextEffect;

            newNode.input = newNode.gameObject.transform.Find("Input").gameObject;
            newNode.output = newNode.gameObject.transform.Find("Output").gameObject;

            NodeManager.GetInstance().AddToList(newNode);

            NodeVisualizeSettingManager.GetInstance().SetNode(newNode);
        }

        public void SetNodeConnectedLineFromLoadData(NodeData nodeData)
        {
            Node node = NodeManager.GetInstance().GetNodeByName(nodeData.nodeId);
            foreach (var inputLine in nodeData.inputLineIdList)
            {
                node.inputLineList.Add(LineManager.GetInstance().GetLine(inputLine));
            }

            foreach (var outputLine in nodeData.outputLineIdList)
            {
                node.outputLineList.Add(LineManager.GetInstance().GetLine(outputLine));
            }
        }
        
    }
}
