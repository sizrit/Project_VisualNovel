using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace StoryBoardEditor
{

    [Serializable]
    public class NodeData
    {
        public string nodeId;
        public string prevNodeId;
        public string nextNodeId;
        public string inputLineId;
        public string outputLineId;
        public string storyBoardId;
        public float x;
        public float y;
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
            SaveNode();
            SaveLine();
            SaveEtc();
        }

        private void SaveNode()
        {
            List<NodeData> nodeDataList = new List<NodeData>();
            foreach (var node in NodeManager.GetInstance().GetAllNode())
            {
                var position = node.gameObject.transform.position;
                NodeData newNodeData = new NodeData
                {
                    nodeId = node.id,
                    storyBoardId = "",
                    nextNodeId = node.nextNode?.id,
                    prevNodeId = node.prevNode?.id,
                    inputLineId = node.inputLine?.id,
                    outputLineId = node.outputLine?.id,
                    x = position.x,
                    y = position.y
                };
                nodeDataList.Add(newNodeData);
            }

            string jsonData = JsonConvert.SerializeObject(nodeDataList, Formatting.Indented);
            string path = Application.dataPath+"/StoryBoardEditorSave/NodeData.json";
            File.WriteAllText(path,jsonData);
        }

        private void SaveLine()
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
            
            string jsonData = JsonConvert.SerializeObject(linDataList, Formatting.Indented);
            string path = Application.dataPath+"/StoryBoardEditorSave/LineData.json";
            File.WriteAllText(path,jsonData);
        }

        private void SaveEtc()
        {
            EtcData newEtcData = new EtcData
            {
                nodeCount = NodeManager.GetInstance().nodeIdCount, lineCount = LineManager.GetInstance().lineCount
            };

            string jsonData = JsonConvert.SerializeObject(newEtcData, Formatting.Indented);
            string path = Application.dataPath+"/StoryBoardEditorSave/EtcData.json";
            File.WriteAllText(path,jsonData);
        }
    }
}
