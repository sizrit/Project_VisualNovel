using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    public class StoryBoardEditorNodeInfoManager : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorNodeInfoManager _instance;

        public static StoryBoardEditorNodeInfoManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorNodeInfoManager>();
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
    
        [SerializeField] private GameObject storyinputField;
        [SerializeField] private GameObject BgId;
        [SerializeField] private GameObject scrollSelectionPrefab;
        [SerializeField] private GameObject selectionLayer;
        private Action<RaycastHit2D[]> _checkSelectionClick = delegate{ };
        private bool _isSelectionModeOn = false;

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

        public void CheckClick(RaycastHit2D[] hits)
        {
            if (_isSelectionModeOn)
            {
                _checkSelectionClick(hits);
                return;
            }

            foreach (var hit in hits)
            {
                if (hit.transform.name == "Apply")
                {
                    string nodeName = StoryBoardEditorClickSystem.GetInstance().GetCurrentSelectedNode().name;
                    StoryBoardNode node = StoryBoardEditorNodeManager.GetInstance().GetNodeByName(nodeName);
                    node.nodeId = this.GetComponentInChildren<InputField>().text;
                    node.nodeObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshPro>().text =
                        node.nodeId;

                    string mode = BgId.GetComponentInChildren<Text>().text;
                    //node.SetStoryBoard( new StoryBoard("",));
                    node.nodeObject.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshPro>().text =
                        node.nodeId;
                }

                if (hit.transform.name == "BgId")
                {
                    GameObject scroll = Instantiate(scrollSelectionPrefab, selectionLayer.transform);
                    scroll.transform.GetComponent<StoryBoardEditorNodeInfoScrollSelectionManager>()
                        .SetScrollSelection(Enum.GetValues(typeof(GameMode)).Cast<GameMode>().ToList(),
                            hit.transform.gameObject);
                }
            }
        }
    }
}

