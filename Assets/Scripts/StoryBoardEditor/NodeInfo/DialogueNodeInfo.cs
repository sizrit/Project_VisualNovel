using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    public class DialogueNodeInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject storyBoardIdCheckBox;
        [SerializeField] private GameObject storyBoardIdCheckBoxImage;
        [SerializeField] private GameObject dialogueTextEffectIdCheckBox;
        [SerializeField] private GameObject dialogueTextEffectIdCheckBoxImage;
        [SerializeField] private Sprite checkOnImage;
        [SerializeField] private Sprite checkOffImage;
        
        [SerializeField] private GameObject storyBoardIdInputField;
        [SerializeField] private GameObject bgIdInputField;
        [SerializeField] private GameObject imageIdInputField;
        [SerializeField] private GameObject speakerInputField;
        [SerializeField] private GameObject dialogueTextInputField;
        [SerializeField] private GameObject dialogueTextEffectIdField;
        
        [SerializeField] private GameObject apply;
        
        public void SetNodeInfo(Node node)
        {
            if (node.isUseStaticStoryBoardId)
            {
                EnableStoryBoardIdZone();
                storyBoardIdInputField.GetComponent<InputField>().text = node.staticStoryBoardId;
            }
            else
            {
                DisableStoryBoardIdZone();
            }

            if (node.isUseTextEffect)
            {
                EnableDialogueTextEffectZone();
                dialogueTextEffectIdField.GetComponent<InputField>().text = node.dialogueTextEffectId.ToString();
            }
            else
            {
                DisableDialogueTextEffectZone();
            }

            if (node.bgId != BgId.Null)
            {
                bgIdInputField.GetComponent<InputField>().text = node.bgId.ToString();
            }
            
            imageIdInputField.GetComponent<InputField>().text = node.imageId;
            speakerInputField.GetComponent<InputField>().text = node.speaker;

            if (node.dialogueTextEffectId != DialogueTextEffectId.Null)
            {
                dialogueTextInputField.GetComponent<InputField>().text = node.dialogueText;
            }
        }

        private void EnableStoryBoardIdZone()
        {
            storyBoardIdCheckBoxImage.GetComponent<Image>().sprite = checkOnImage;
            storyBoardIdInputField.GetComponent<Image>().color = Color.white;
            storyBoardIdInputField.GetComponent<InputField>().enabled = true;
        }

        private void DisableStoryBoardIdZone()
        {
            storyBoardIdCheckBoxImage.GetComponent<Image>().sprite = checkOffImage;
            storyBoardIdInputField.GetComponent<Image>().color = Color.gray;
            storyBoardIdInputField.GetComponent<InputField>().enabled = false;
        }

        private void EnableDialogueTextEffectZone()
        {
            dialogueTextEffectIdCheckBoxImage.GetComponent<Image>().sprite = checkOnImage;
            dialogueTextEffectIdField.GetComponent<Image>().color = Color.white;
            dialogueTextEffectIdField.GetComponent<InputField>().enabled = true;
        }
        
        private void DisableDialogueTextEffectZone()
        {
            dialogueTextEffectIdCheckBoxImage.GetComponent<Image>().sprite = checkOffImage;
            dialogueTextEffectIdField.GetComponent<Image>().color = Color.gray;
            dialogueTextEffectIdField.GetComponent<InputField>().enabled = false;
        }
        
        public void Click(RaycastHit2D[] hits)
        {
            Node node = NodeManipulator.GetInstance().GetSelectedNode();
            
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == storyBoardIdCheckBox)
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

                if (hit.transform.gameObject == dialogueTextEffectIdCheckBox)
                {
                    node.isUseTextEffect = !node.isUseTextEffect;
                    if (node.isUseTextEffect)
                    {
                        EnableDialogueTextEffectZone();
                    }
                    else
                    {
                        DisableDialogueTextEffectZone();
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
                node.staticStoryBoardId = storyBoardIdInputField.GetComponent<InputField>().text;
            }
            else
            {
                node.staticStoryBoardId = "";
            }

            node.bgId = StoryBoardBgLoadManager.ConvertToBgId(bgIdInputField.GetComponent<InputField>().text);

            node.imageId = imageIdInputField.GetComponent<InputField>().text;

            node.speaker = speakerInputField.GetComponent<InputField>().text;

            node.dialogueText = dialogueTextInputField.GetComponent<InputField>().text;

            if (node.isUseTextEffect)
            {
                node.dialogueTextEffectId = DialogueTextEffectManager.ConvertStringToDialogueTextEffectId(dialogueTextEffectIdField.GetComponent<InputField>().text);
            }
            else
            {
                node.dialogueTextEffectId = DialogueTextEffectId.Null;
            }
            
            NodeVisualizeSettingManager.GetInstance().SetNode(node);
        }
    }
}
 