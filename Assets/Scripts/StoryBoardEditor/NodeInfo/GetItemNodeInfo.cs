using StoryBoardEditor.Node_ScriptAsset;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    public class GetItemNodeInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject storyBoardIdCheckBox;
        [SerializeField] private GameObject storyBoardIdCheckBoxImage;
        [SerializeField] private Sprite checkOnImage;
        [SerializeField] private Sprite checkOffImage;

        [SerializeField] private GameObject storyBoardIdInputField;
        [SerializeField] private GameObject itemInputField;

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

            itemInputField.GetComponent<InputField>().text =
                node.itemId != Item.Null ? node.itemId.ToString() : "";
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
            }
            else
            {
                node.staticStoryBoardId = "";
            }

            node.itemId = ItemManager.ConvertToItem(itemInputField.GetComponent<InputField>().text);

            NodeVisualizeSettingManager.GetInstance().SetNode(node);
        }
    }
}