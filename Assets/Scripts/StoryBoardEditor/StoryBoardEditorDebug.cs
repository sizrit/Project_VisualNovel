using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorDebug : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorDebug _instance;

        public static StoryBoardEditorDebug GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorDebug>();
                if (obj == null)
                {
                    Debug.LogError("StoryBoardEditorDebug Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion

        public void Log()
        {
            List<Node> nodeList = NodeManager.GetInstance().GetAllNode();
            List<Line> lineList = LineManager.GetInstance().GetAllLine();
            string log = "Node List\n-------------------------\n";
            foreach (var node in nodeList)
            {
                FieldInfo[] infoList =
                    typeof(Node).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var info in infoList)
                {
                    log += info.Name;
                    log += " : ";
                    if (info.GetValue(node) != null)
                    {
                        log += info.GetValue(node).ToString();
                    }
                    
                    log += "\n";
                }
                
                //"\nInputLineList\n";
                foreach (var inputLine in node.inputLineList)
                {
                    log = log + "Line Id : " + inputLine.id +"\n"; 
                }

                log = log + "OutputLineList\n";
                foreach (var outputLine in node.outputLineList)
                {
                    log = log + "Line Id : " + outputLine.id +" / Node01 : "+ outputLine.node01.id +  " / Node02 : " + outputLine.node02.id  +"\n"; 
                }

                log += "\n\n";
            }

            Debug.Log(log);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Log();
            }
        }
    }
}
