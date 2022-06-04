using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StoryBoardEditor.Line;
using StoryBoardEditor.Node;
using UnityEngine;

namespace StoryBoardEditor
{
    [Serializable]
    public class SaveData
    {
        public List<NodeData> nodeData = new List<NodeData>();
        public List<LineData> lineData = new List<LineData>();
        public EtcData etcData = new EtcData();
    }
    
    [Serializable]
    public class NodeData
    {
        public string nodeId;
        
        public float x;
        public float y;
        
        public string type;
        
        public bool isUseStaticStoryBoardId;
        public string staticStoryBoardId;

        public List<string> inputLineIdList =new List<string>() ;
        public List<string> outputLineIdList =new List<string>();

        public string bgId;
        public string imageId;
        public string dialogueText;
        public string speaker;

        public bool isUseTextEffect;
        public string dialogueTextEffectId;

        public string selectionId;

        public string selectionText;
        
        public string clueId;

        public string itemId;

        public string eventId;
    }

    [Serializable]
    public class LineData
    {
        public string lineId;
        public string node01Id;
        public string node02Id;
    }

    [Serializable]
    public class EtcData
    {
        public int nodeCount;
        public int lineCount;
    }
    
    public class SaveManager
    {
        #region Singleton

        private static SaveManager _instance;

        public static SaveManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }

        #endregion

        public void Save()
        {
            SaveData saveData = new SaveData {nodeData = SaveNode(), lineData = SaveLine(), etcData = SaveEtc()};

            string jsonData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            string path = Application.dataPath + "/StoryBoardEditorSave/SaveData.json";
            File.WriteAllText(path, jsonData);
        }

        private List<NodeData> SaveNode()
        {
            List<NodeData> nodeDataList = new List<NodeData>();
            foreach (var node in NodeManager.GetInstance().GetAllNode())
            {
                var position = node.gameObject.transform.position;
                NodeData newNodeData = new NodeData
                {
                    nodeId = node.id,

                    isUseStaticStoryBoardId = node.isUseStaticStoryBoardId,

                    staticStoryBoardId = node.staticStoryBoardId,

                    x = position.x,
                    y = position.y,

                    type = node.type.ToString(),
                    
                    bgId = node.bgId.ToString(),
                    imageId = node.imageId.ToString(),
                    dialogueText = node.dialogueText,
                    speaker = node.speaker,
                    isUseTextEffect = node.isUseTextEffect,
                    
                    selectionId = node.selectionId,
                    
                    selectionText = node.selectionText,
                    
                    clueId = node.clueId.ToString(),
                    itemId = node.itemId.ToString(),
                    eventId = node.eventId.ToString()
                };
                
                foreach (var inputLine in node.inputLineList)
                {
                    newNodeData.inputLineIdList.Add(inputLine.id);
                }

                foreach (var outputLine in node.outputLineList)
                {
                    newNodeData.outputLineIdList.Add(outputLine.id);
                }
                    
                nodeDataList.Add(newNodeData);
            }

            return nodeDataList;
        }

        private List<LineData> SaveLine()
        {
            List<LineData> linDataList = new List<LineData>();
            foreach (var line in LineManager.GetInstance().GetAllLine())
            {
                LineData newLineData = new LineData
                {
                    lineId = line.id, node01Id = line.node01.id, node02Id = line.node02.id
                };
                linDataList.Add(newLineData);
            }

            return linDataList;
        }

        private EtcData SaveEtc()
        {
            EtcData newEtcData = new EtcData
            {
                nodeCount = NodeManager.GetInstance().nodeIdCount, lineCount = LineManager.GetInstance().lineCount
            };

            return newEtcData;
        }
    }
}
