using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class StoryBoardEditorUI_EditButton : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorUI_EditButton _instance;

        public static StoryBoardEditorUI_EditButton GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorUI_EditButton>();
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
        }

        public void Click()
        {
            _isEditModeOn = !_isEditModeOn;
            UpdateImage();
            if (!_isEditModeOn)
            {
                StoryBoardEditorNodeInfoManager.GetInstance().DisableNodeInfo();
            }
        }

        public bool GetIsEditModeOn()
        {
            return _isEditModeOn;
        }

        private void UpdateImage()
        {
            checkImageObject.GetComponent<Image>().sprite = _isEditModeOn ? checkImage : nullImage;
        }
    }
}
