using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace StoryBoardEditor
{
    public class LoadManager
    {
        #region Singleton

        private static LoadManager _instance;

        public static LoadManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LoadManager();
            }
            return _instance;
        }

        #endregion

        public void Load()
        {
            NodeManager.GetInstance().ClearAllNode();
            LineManager.GetInstance().ClearAllLine();
            List<NodeData> nodeDataList = LoadNode();
            List<LineData> lineDataLis = LoadLine();
            SetNode(nodeDataList);
            SetLine(lineDataLis);
            LoadEtc();
        }

        private List<NodeData> LoadNode()
        {
            string path = Application.dataPath + "/StoryBoardEditorSave/NodeData.json";
            string jsonData = File.ReadAllText(path);
            List<NodeData> nodeDataList = JsonConvert.DeserializeObject<List<NodeData>>(jsonData);
            foreach (var nodeData in nodeDataList)
            {
                NodeManager.GetInstance().MakeNodeFromLoadData(nodeData);
            }

            return nodeDataList;
        }

        private List<LineData> LoadLine()
        {
            string path = Application.dataPath + "/StoryBoardEditorSave/LineData.json";
            string jsonData = File.ReadAllText(path);
            List<LineData> lineDataList = JsonConvert.DeserializeObject<List<LineData>>(jsonData);
            foreach (var lineData in lineDataList)
            {
                LineManager.GetInstance().MakeLineFromLoadData(lineData);
            }

            return lineDataList;
        }

        private void SetNode(List<NodeData> nodeDataList)
        {
            foreach (var nodeData in nodeDataList)
            {
                NodeManager.GetInstance().SetNodeFromLoadData(nodeData);
            }
        }

        private void SetLine(List<LineData> lineDataList)
        {
            foreach (var lineData in lineDataList)
            {
                LineManager.GetInstance().SetLineFromLoadData(lineData);
            }
        }

        private void LoadEtc()
        {
            string path = Application.dataPath + "/StoryBoardEditorSave/EtcData.json";
            string jsonData = File.ReadAllText(path);
            EtcData etcData = JsonConvert.DeserializeObject<EtcData>(jsonData);
            NodeManager.GetInstance().nodeIdCount = etcData.nodeCount + 1;
            LineManager.GetInstance().lineCount = etcData.lineCount + 1;
        }
    }
}
