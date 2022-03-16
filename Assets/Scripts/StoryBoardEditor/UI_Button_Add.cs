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

        Action _updateFunc =delegate { };
    
        public void Click()
        {
            ClickSystem.GetInstance().DisableClick();
            ClickSystem.GetInstance().SubscribeCustomClick(AddNode);
            
            UI_ButtonManager.GetInstance().DisableAllUI_Button();
            
            shadowNode.SetActive(true);
            _updateFunc += ShadowNodeEffect;
        }

        private void ShadowNodeEffect()
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            shadowNode.transform.position = position;
        }

        private void AddNode(RaycastHit2D[] hits)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            NodeManager.GetInstance().AddNode(position);
            shadowNode.SetActive(false);
            
            ClickSystem.GetInstance().UnsubscribeCustomClick(AddNode);
            ClickSystem.GetInstance().EnableClick();
            
            UI_ButtonManager.GetInstance().EnableAllUI_Button();
        }
    
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            _updateFunc();
        }
    }
}
