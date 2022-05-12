using System;
using System.Diagnostics.CodeAnalysis;
using StoryBoardEditor.NodeInfo;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UI_EditModeButton : MonoBehaviour
    {
        #region Singleton

        private static UI_EditModeButton _instance;

        public static UI_EditModeButton GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<UI_EditModeButton>();
                if (obj == null)
                {
                    Debug.LogError("StoryBoardEditorUIEditButton is not ready");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }

        #endregion
        
        private bool _isEditModeOn = false;
        [SerializeField] private GameObject checkImageObject;
        [SerializeField] private Sprite nullImage;
        [SerializeField] private Sprite checkImage;

        private void OnEnable()
        {
            UpdateImage();
            UpdateNodeInfoManager();
        }

        public void Click()
        {
            _isEditModeOn = !_isEditModeOn;
            UpdateImage();
            UpdateNodeInfoManager();
        }

        private void UpdateImage()
        {
            checkImageObject.GetComponent<Image>().sprite = _isEditModeOn ? checkImage : nullImage;
        }

        private void UpdateNodeInfoManager()
        {
            if (_isEditModeOn)
            {
                NodeInfoManager.GetInstance().EnableNodeInfo();
            }
            else
            {
                NodeInfoManager.GetInstance().DisableNodeInfo();
            }
        } 
    }
}
