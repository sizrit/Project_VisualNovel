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
                Debug.Log(node.staticStoryBoardId);
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
                Debug.Log(node.dialogueTextEffectId.ToString());
            }
            else
            {
                DisableDialogueTextEffectZone();
            }

            if (node.storyBoard.bgId != BgId.Null)
            {
                bgIdInputField.GetComponent<InputField>().text = node.storyBoard.bgId.ToString();
            }
            
            imageIdInputField.GetComponent<InputField>().text = node.storyBoard.imageId;
            speakerInputField.GetComponent<InputField>().text = node.speaker;
            dialogueTextInputField.GetComponent<InputField>().text = node.dialogueText;
            
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
                node.gameObject.transform.Find("Member01").Find("InputText").GetComponent<TMP_Text>().text =
                    node.staticStoryBoardId;
                node.gameObject.transform.Find("Member01").Find("InputText").Find("TextBox").GetComponent<SpriteRenderer>().color =Color.white;
                node.gameObject.transform.Find("Member01").Find("CheckBox").GetComponent<SpriteRenderer>().sprite = checkOnImage;
            }
            else
            {
                node.staticStoryBoardId = "";
                node.gameObject.transform.Find("Member01").Find("InputText").GetComponent<TMP_Text>().text = "";
                node.gameObject.transform.Find("Member01").Find("InputText").Find("TextBox").GetComponent<SpriteRenderer>().color =Color.gray;
                node.gameObject.transform.Find("Member01").Find("CheckBox").GetComponent<SpriteRenderer>().sprite = checkOffImage;
            }

            node.storyBoard.bgId =
                StoryBoardBgLoadManager.ConvertToBgId(bgIdInputField.GetComponent<InputField>().text);
            node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text =
                node.storyBoard.bgId.ToString();
            
            node.storyBoard.imageId = imageIdInputField.GetComponent<InputField>().text;
            node.gameObject.transform.Find("Member03").Find("InputText").GetComponent<TMP_Text>().text =
                node.storyBoard.imageId;
            
            node.speaker = speakerInputField.GetComponent<InputField>().text;
            node.gameObject.transform.Find("Member04").Find("InputText").GetComponent<TMP_Text>().text =
                node.speaker;
            
            node.dialogueText = dialogueTextInputField.GetComponent<InputField>().text;
            node.gameObject.transform.Find("Member05").Find("InputText").GetComponent<TMP_Text>().text =
                node.dialogueText;
            
            if (node.isUseTextEffect)
            {
                node.dialogueTextEffectId = DialogueTextEffectManager.ConvertStringToDialogueTextEffectId(dialogueTextEffectIdField.GetComponent<InputField>().text);
                node.gameObject.transform.Find("Member06").Find("InputText").GetComponent<TMP_Text>().text =
                    node.dialogueTextEffectId.ToString();
                node.gameObject.transform.Find("Member06").Find("InputText").Find("TextBox").GetComponent<SpriteRenderer>().color =Color.white;
                node.gameObject.transform.Find("Member06").Find("CheckBox").GetComponent<SpriteRenderer>().sprite = checkOnImage;
            }
            else
            {
                node.dialogueTextEffectId = DialogueTextEffectId.Null;
                node.gameObject.transform.Find("Member06").Find("InputText").GetComponent<TMP_Text>().text = "";
                node.gameObject.transform.Find("Member06").Find("InputText").Find("TextBox").GetComponent<SpriteRenderer>().color =Color.gray;
                node.gameObject.transform.Find("Member06").Find("CheckBox").GetComponent<SpriteRenderer>().sprite = checkOffImage;
            }
        }
    }
}
 