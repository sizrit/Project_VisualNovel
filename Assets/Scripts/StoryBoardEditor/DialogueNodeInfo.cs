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
        
        [SerializeField] private GameObject storyBoardIdInputField;
        [SerializeField] private GameObject bgIdInputField;
        [SerializeField] private GameObject imageIdInputField;
        [SerializeField] private GameObject speakerInputField;
        [SerializeField] private GameObject textInputField;
        [SerializeField] private GameObject textEffectIdField;
        
        [SerializeField] private GameObject apply;
        
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
            Debug.Log(node.storyBoard.imageId);

            
            imageIdInputField.GetComponent<InputField>().text = node.storyBoard.imageId;
            speakerInputField.GetComponent<InputField>().text = node.storyBoard.d
        }

        private void EnableStoryBoardIdZone()
        {
            checkBoxImage.GetComponent<Image>().sprite = checkOnImage;
            storyBoardIdInputField.GetComponent<Image>().color = Color.white;
            storyBoardIdInputField.GetComponent<TMP_InputField>().enabled = true;
        }

        private void DisableStoryBoardIdZone()
        {
            checkBoxImage.GetComponent<Image>().sprite = checkOffImage;
            storyBoardIdInputField.GetComponent<Image>().color = Color.gray;
            storyBoardIdInputField.GetComponent<TMP_InputField>().enabled = false;
        }

        public void Click(RaycastHit2D[] hits)
        {
            Node node = NodeManipulator.GetInstance().GetSelectedNode();
            
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == checkBox)
                {
                    node.isUseStaticStoryBoardId = !node.isUseStaticStoryBoardId;
                    if (node.isUseStaticStoryBoardId)
                    {
                        EnableStoryBoardIdZone();
                    }
                    else
                    {
                        DisableStoryBoardIdZone();
                    }
                }

                if (hit.transform.gameObject == apply)
                {
                    ApplyData();
                }
            }
        }
        
        private void ApplyData()
        {
            Debug.Log("apply");
            
            Node node = NodeManipulator.GetInstance().GetSelectedNode();
            
            if (node.isUseStaticStoryBoardId)
            {
                node.storyBoard.storyBoardId = storyBoardIdInputField.GetComponent<InputField>().text;
            }
            else
            {
                node.storyBoard.storyBoardId = "";
            }
            
            node.storyBoard.bgId = StoryBoardBgLoadManager.GetInstance()
                .ConvertToBgId(bgIdInputField.GetComponent<InputField>().text);
            
            node.storyBoard.imageId = imageIdInputField.GetComponent<InputField>().text;

        }
        
        
        
    }
}
