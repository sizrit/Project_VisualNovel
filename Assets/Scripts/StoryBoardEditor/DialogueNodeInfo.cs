using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor
{
    public class DialogueNodeInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject checkBox;
        [SerializeField] private GameObject checkBoxImage;
        [SerializeField] private Sprite checkOnImage;
        [SerializeField] private Sprite checkOffImage;
        
        [SerializeField] private GameObject storyBoardIdText;
        [SerializeField] private GameObject storyBoardIdImage;
        [SerializeField] private GameObject storyBoardIdInputField;
        
        [SerializeField] private GameObject bgIdText;
        [SerializeField] private GameObject bgIdInputField;
        
        [SerializeField] private GameObject imageIdText;
        [SerializeField] private GameObject imageIdInputField;
        
        public void SetNodeInfo(Node node)
        {
            if (node.isUseStaticStoryBoardId)
            {
                EnableStoryBoardIdZone();
            }
            else
            {
                DisableStoryBoardIdZone();
            }
        }

        private void EnableStoryBoardIdZone()
        {
            checkBoxImage.GetComponent<Image>().sprite = checkOnImage;
        }

        private void DisableStoryBoardIdZone()
        {
            checkBoxImage.GetComponent<Image>().sprite = checkOffImage;
            storyBoardIdInputField.GetComponent<TMP_InputField>().enabled = false;
        }

        public void Click(RaycastHit2D[] hits)
        {
            
        }
    }
}
