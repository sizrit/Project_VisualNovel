using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    public class GetClueNodeInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject storyBoardIdCheckBox;
        [SerializeField] private GameObject storyBoardIdCheckBoxImage;
        [SerializeField] private Sprite checkOnImage;
        [SerializeField] private Sprite checkOffImage;
        
        [SerializeField] private GameObject storyBoardIdInputField;
        [SerializeField] private GameObject clueIdInputField;
        
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

            clueIdInputField.GetComponent<InputField>().text = node.clueId.ToString() ;
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
            
            node.clueId = ClueManager.ConvertToClue(clueIdInputField.GetComponent<InputField>().text);
            node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text = node.clueId.ToString();
        }
    }
}
