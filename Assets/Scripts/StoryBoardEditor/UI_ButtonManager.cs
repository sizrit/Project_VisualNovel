using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum UI_Button
    {
        Add,
        Delete,
        Save,
        Load,
        Clear
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UI_ButtonManager : MonoBehaviour
    {
        #region Singleton

        private static UI_ButtonManager _instance;

        public static UI_ButtonManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<UI_ButtonManager>();
                if (obj == null)
                {
                    Debug.LogError("UI_Button Script is not available!");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }
        
        #endregion

        [SerializeField] private GameObject add;
        [SerializeField] private GameObject delete;
        [SerializeField] private GameObject save;
        [SerializeField] private GameObject load;
        [SerializeField] private GameObject clear;

        private Dictionary<UI_Button, GameObject> uiList = new Dictionary<UI_Button, GameObject>();

        public void UI_Click(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == add)
                {
                    UI_Button_Add.GetInstance().Click();
                }

                if (hit.transform.gameObject == delete)
                {
                    Node currentNode = NodeManipulator.GetInstance().GetSelectedNode();
                    Line currentLine = LineManipulator.GetInstance().GetSelectedLine();

                    if (currentNode != null)
                    {
                        NodeManager.GetInstance().RemoveNode(currentNode);
                    }

                    if (currentLine != null)
                    {
                        LineManager.GetInstance().RemoveLine(currentLine);
                    }
                    
                }

                if (hit.transform.gameObject == save)
                {
                    SaveManager.GetInstance().Save();
                }

                if (hit.transform.gameObject == load)
                {
                    LoadManager.GetInstance().Load();
                }

                if (hit.transform.gameObject == clear)
                {
                    NodeManager.GetInstance().ClearAllNode();
                    LineManager.GetInstance().ClearAllLine();
                }
            }

        }

        public void DisableAllUI_Button()
        {
            foreach (var ui in uiList)
            {
                DisableUI_Button(ui.Key);
            }
        }
        
        public void DisableUI_Button(UI_Button ui)
        {
            Color color = uiList[ui].GetComponent<Text>().color;
            color.a = 0.2f;
            uiList[ui].GetComponent<Text>().color = color;
            uiList[ui].GetComponent<BoxCollider2D>().enabled = false;
        }

        public void RequestDisableDeleteUIButton()
        {
            Node currentNode = NodeManipulator.GetInstance().GetSelectedNode();
            Line currentLine = LineManipulator.GetInstance().GetSelectedLine();

            if (currentLine == null && currentNode == null)
            {
                Color color = uiList[UI_Button.Delete].GetComponent<Text>().color;
                color.a = 0.2f;
                uiList[UI_Button.Delete].GetComponent<Text>().color = color;
                uiList[UI_Button.Delete].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        
        public void RequestEnableDeleteUIButton()
        {
            Node currentNode = NodeManipulator.GetInstance().GetSelectedNode();
            Line currentLine = LineManipulator.GetInstance().GetSelectedLine();

            if (currentLine != null || currentNode != null)
            {
                Color color = uiList[UI_Button.Delete].GetComponent<Text>().color;
                color.a = 1f;
                uiList[UI_Button.Delete].GetComponent<Text>().color = color;
                uiList[UI_Button.Delete].GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        public void EnableAllUI_Button()
        {
            foreach (var ui in uiList)
            {
                EnableUI_Button(ui.Key);
            }
        }

        public void EnableUI_Button(UI_Button ui)
        {
            Color color = uiList[ui].GetComponent<Text>().color;
            color.a = 1f;
            uiList[ui].GetComponent<Text>().color = color;
            uiList[ui].GetComponent<BoxCollider2D>().enabled = true;
        }

        private void Start()
        {
            uiList.Add(UI_Button.Add, add);
            uiList.Add(UI_Button.Delete,delete);
            uiList.Add(UI_Button.Save,save);
            uiList.Add(UI_Button.Load,load);
            uiList.Add(UI_Button.Clear,clear);
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
