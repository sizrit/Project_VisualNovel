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

        [SerializeField] private GameObject enableLayer;
        
        [SerializeField] private GameObject storyBoardModeObject;
        [SerializeField] private GameObject bgIdObject;
        [SerializeField] private GameObject imageIdObject;
        [SerializeField] private GameObject apply;
        
        [SerializeField] private GameObject scrollSelectionPrefab;
        [SerializeField] private GameObject selectionLayer;
        private Action<RaycastHit2D[]> _checkSelectionClick = delegate{ };
        private bool _isSelectionModeOn = false;

        private Node _currentSelectedNode = null;

        private void OnEnable()
        {
            DisableNodeInfo();
        }

        public void SelectionModeOn(Action<RaycastHit2D[]> func)
        {
            _checkSelectionClick = func;
            _isSelectionModeOn = true;
        }

        public void SelectionModeOff()
        {
            _checkSelectionClick = delegate { };
            _isSelectionModeOn = false;
        }

        public void DisableNodeInfo()
        {
            _currentSelectedNode = null;
            enableLayer.SetActive(false);
        }

        public void EnableNodeInfo(Node node)
        {
            if (!UI_EditButton.GetInstance().GetIsEditModeOn())
            {
                DisableNodeInfo();
                return;
            }
            _currentSelectedNode = node;
            enableLayer.SetActive(true);
            SetNodeInfo();
        }

        private void SetNodeInfo()
        {
            storyBoardModeObject.GetComponentInChildren<Text>().text = _currentSelectedNode.storyBoard.mode.ToString();
            bgIdObject.GetComponentInChildren<Text>().text = _currentSelectedNode.storyBoard.bgId.ToString();
            imageIdObject.GetComponentInChildren<Text>().text = _currentSelectedNode.storyBoard.imageId;
            imageIdObject.GetComponent<InputField>().text = _currentSelectedNode.storyBoard.imageId;
        }

        private T StringToEnum<T>(string stringValue)
        {
            List<T> enumList = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            foreach (var e in enumList)
            {
                if (e.ToString() == stringValue)
                {
                    return e;
                }
            }

            Debug.LogError(stringValue+" dose not match with "+typeof(T).Name);
            return enumList[0];
        }

        public void CheckClick(RaycastHit2D[] hits)
        {
            if (_isSelectionModeOn)
            {
                _checkSelectionClick(hits);
                return;
            }

            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == apply)
                {
                    string imageId = imageIdObject.transform.GetComponentInChildren<InputField>().text;
                    StoryBoardMode mode = StringToEnum<StoryBoardMode>(storyBoardModeObject.GetComponentInChildren<Text>().text);
                    BgId bgId = StringToEnum<BgId>(bgIdObject.GetComponentInChildren<Text>().text);

                    _currentSelectedNode.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshPro>().text =
                        mode.ToString();
                    _currentSelectedNode.gameObject.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshPro>().text =
                        bgId.ToString();
                    _currentSelectedNode.gameObject.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TextMeshPro>().text =
                        imageId;

                    _currentSelectedNode.storyBoard = new StoryBoard("", mode, bgId, imageId, "");
                }
                
                if (hit.transform.gameObject == storyBoardModeObject)
                {
                    GameObject scroll = Instantiate(scrollSelectionPrefab, selectionLayer.transform);
                    scroll.transform.GetComponent<NodeInfoScrollSelectionManager>()
                        .SetScrollSelection(Enum.GetValues(typeof(StoryBoardMode)).Cast<StoryBoardMode>().ToList(),
                            hit.transform.gameObject);
                    scroll.transform.position = hit.transform.position;
                }

                if (hit.transform.gameObject == bgIdObject)
                {
                    GameObject scroll = Instantiate(scrollSelectionPrefab, selectionLayer.transform);
                    scroll.transform.GetComponent<NodeInfoScrollSelectionManager>()
                        .SetScrollSelection(Enum.GetValues(typeof(BgId)).Cast<BgId>().ToList(),
                            hit.transform.gameObject);
                    scroll.transform.position = hit.transform.position;
                }
            }
            
            ;
        }
    }
}

