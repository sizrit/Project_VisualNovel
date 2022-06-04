using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StoryBoardEditor.Line;
using StoryBoardEditor.Node;
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

            string path = Application.dataPath + "/StoryBoardEditorSave/SaveData.json";
            string jsonData = File.ReadAllText(path);
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            
            LoadNode(saveData.nodeData);
            LoadLine(saveData.lineData);
            LoadEtc(saveData.etcData);
            
            SetNode(saveData.nodeData);
            SetLine(saveData.lineData);
        }

        private void LoadNode(List<NodeData> nodeDataList)
        {
            foreach (var nodeData in nodeDataList)
            {
                LoadNodeManager.GetInstance(). MakeNodeFromLoadData(nodeData);
            }
        }

        private void LoadLine(List<LineData> lineDataList)
        {
            foreach (var lineData in lineDataList)
            {
                LineManager.GetInstance().MakeLineFromLoadData(lineData);
            }
        }

        private void SetNode(List<NodeData> nodeDataList)
        {
            foreach (var nodeData in nodeDataList)
            {
                LoadNodeManager.GetInstance().SetNodeConnectedLineFromLoadData(nodeData);
            }
        }

        private void SetLine(List<LineData> lineDataList)
        {
            foreach (var lineData in lineDataList)
            {
                LineManager.GetInstance().SetLineFromLoadData(lineData);
            }
        }

        private void LoadEtc(EtcData etcData)
        {
            NodeManager.GetInstance().nodeIdCount = etcData.nodeCount + 1;
            LineManager.GetInstance().lineCount = etcData.lineCount + 1;
        }
    }
}
