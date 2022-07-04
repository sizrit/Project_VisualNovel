using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StoryBoardEditor.Line_ScriptAsset;
using StoryBoardEditor.Node_ScriptAsset;
using StoryBoardSystem;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorDataConvertor : MonoBehaviour
    {
        [SerializeField] private TextAsset saveDataFile;
        

        public void Convert()
        {
            string path = Application.dataPath + "/StoryBoardEditorSave/SaveData.json";
            string jsonData = File.ReadAllText(path);

            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);

            Dictionary<string, Node> nodeList = new Dictionary<string, Node>();
            Dictionary<string, Line> lineList = new Dictionary<string, Line>();

            foreach (var nodeData in saveData.nodeData)
            {
                nodeList.Add(nodeData.nodeId, LoadNodeManager.ConvertNodeDataToNode(nodeData));
            }

            foreach (var lineData in saveData.lineData)
            {
                Line newLine = new Line();
                newLine.id = lineData.lineId;
                newLine.node01 = nodeList[lineData.node01Id];
                newLine.node02 = nodeList[lineData.node02Id];
                lineList.Add(newLine.id, newLine);
            }

            foreach (var nodeData in saveData.nodeData)
            {
                foreach (var inputLineId in nodeData.inputLineIdList)
                {
                    nodeList[nodeData.nodeId].inputLineList.Add(lineList[inputLineId]);
                }

                foreach (var outputLineId in nodeData.outputLineIdList)
                {
                    nodeList[nodeData.nodeId].outputLineList.Add(lineList[outputLineId]);
                }
            }

            Dictionary<string, StoryBoard> storyBoardList = new Dictionary<string, StoryBoard>();
            foreach (var nodePair in nodeList)
            {
                Node node = nodePair.Value;
                
                StoryBoard newStoryBoard = new StoryBoard();

                newStoryBoard.mode = ConvertNodeTypeToStoryBoardMode(node.type);
                
                newStoryBoard.storyBoardId = node.staticStoryBoardId;

                newStoryBoard.bgId = node.bgId;
                newStoryBoard.imageId = node.imageId;
                
                newStoryBoard.selectionId = node.selectionId;
                newStoryBoard.clueId = node.clueId;
                newStoryBoard.eventId = node.eventId;

                storyBoardList.Add(node.id, newStoryBoard);
            }

            foreach (var node in nodeList)
            {
                if (node.Value.type != NodeType.Selection && node.Value.type != NodeType.SelectionText)
                {
                    foreach (var outputLine in node.Value.outputLineList)
                    {
                        Node nextNode = outputLine.node02;
                        storyBoardList[node.Value.id].nextStoryBoard = storyBoardList[nextNode.id];
                    }
                }
            }

            foreach (var storyBoard in storyBoardList)
            {
                Debug.Log(storyBoard.Key+ " : " + storyBoard.Value.storyBoardId);
            }
        }

        private void MakeSelectionEvent()
        {
            
        }

        private StoryBoardMode ConvertNodeTypeToStoryBoardMode(NodeType type)
        {
            switch (type)
            {
                case NodeType.Dialogue:
                    return StoryBoardMode.Dialogue;

                case NodeType.Selection:
                    return StoryBoardMode.Selection;

                case NodeType.GetClue:
                    return StoryBoardMode.GetClue;

                case NodeType.GetItem:
                    return StoryBoardMode.GetItem;

                case NodeType.Event:
                    return StoryBoardMode.Event;
            }

            return StoryBoardMode.Null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Convert();
            }
        }
    }
}