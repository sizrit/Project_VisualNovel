using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class UI_Button_Add : MonoBehaviour
    {
        #region Singleton

        private static UI_Button_Add _instance;

        public static UI_Button_Add GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<UI_Button_Add>();
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
        
        [SerializeField] private GameObject shadowNode;
        [SerializeField] private GameObject dialogueNodeButton;
        [SerializeField] private GameObject selectionNodeButton;
        [SerializeField] private GameObject selectionTextNodeButton;
        [SerializeField] private GameObject nodeTypePanel;
        private NodeType _type = NodeType.Dialogue;
        
        Action _updateFunc =delegate { };
    
        public void Click()
        {
            ClickSystem.GetInstance().DisableClick();
            ClickSystem.GetInstance().SubscribeCustomClick(SelectNodeType);
            
            UI_ButtonManager.GetInstance().DisableAllUI_Button();
            EnableNodeTypePanel();
        }

        private void ShadowNodeEffect()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            shadowNode.transform.position = position;
        }

        private void EnableNodeTypePanel()
        {
            dialogueNodeButton.SetActive(true);
            selectionNodeButton.SetActive(true);
        }

        private void DisableNodeTypePanel()
        {
            dialogueNodeButton.SetActive(false);
            selectionNodeButton.SetActive(false);
        }

        private void SelectNodeType(RaycastHit2D[] hits ,RaycastHit[] none)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == dialogueNodeButton)
                {
                    _type = NodeType.Dialogue;
                }

                if (hit.transform.gameObject == selectionNodeButton)
                {
                    _type = NodeType.Selection;
                }

                if (hit.transform.gameObject == selectionTextNodeButton)
                {
                    _type = NodeType.SelectionText;
                }

                ClickSystem.GetInstance().UnsubscribeCustomClick(SelectNodeType);
                ClickSystem.GetInstance().SubscribeCustomClick(AddNode);

                DisableNodeTypePanel();

                shadowNode.SetActive(true);
                _updateFunc += ShadowNodeEffect;
            }

            ClickSystem.GetInstance().UnsubscribeCustomClick(SelectNodeType);
            ClickSystem.GetInstance().EnableClick();
            
            UI_ButtonManager.GetInstance().EnableAllUI_Button();
            DisableNodeTypePanel();
        }

        private void AddNode(RaycastHit2D[] hits, RaycastHit[] none)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            NodeManager.GetInstance().AddNode(position, _type);
            shadowNode.SetActive(false);
            
            ClickSystem.GetInstance().UnsubscribeCustomClick(AddNode);
            ClickSystem.GetInstance().EnableClick();
            
            UI_ButtonManager.GetInstance().EnableAllUI_Button();
        }
    
        void Start()
        {
            DisableNodeTypePanel();
        }
        
        void Update()
        {
            _updateFunc();
        }
    }
}
