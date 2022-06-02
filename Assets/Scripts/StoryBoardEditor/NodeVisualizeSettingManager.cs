using StoryBoardSystem;
using TMPro;
using UnityEngine;

namespace StoryBoardEditor
{
    public class NodeVisualizeSettingManager : MonoBehaviour
    {
        #region Singleton

        private static NodeVisualizeSettingManager _instance;

        public static NodeVisualizeSettingManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<NodeVisualizeSettingManager>();
                if (obj == null)
                {
                    Debug.LogError("NodeVisualizeSettingManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        [SerializeField] private Sprite checkOnImage;
        [SerializeField] private Sprite checkOffImage;
        
        public void SetNode(Node node)
        {
            NodeType type = node.type;
            
            if (type != NodeType.SelectionText)
            {
                if (node.isUseStaticStoryBoardId)
                {
                    node.gameObject.transform.Find("Member01").Find("InputText").GetComponent<TMP_Text>().text =
                        node.staticStoryBoardId;
                    node.gameObject.transform.Find("Member01").Find("InputText").Find("TextBox")
                        .GetComponent<SpriteRenderer>().color = Color.white;
                    node.gameObject.transform.Find("Member01").Find("CheckBox").GetComponent<SpriteRenderer>().sprite =
                        checkOnImage;
                }
                else
                {
                    node.staticStoryBoardId = "";
                    node.gameObject.transform.Find("Member01").Find("InputText").GetComponent<TMP_Text>().text = "";
                    node.gameObject.transform.Find("Member01").Find("InputText").Find("TextBox")
                        .GetComponent<SpriteRenderer>().color = Color.gray;
                    node.gameObject.transform.Find("Member01").Find("CheckBox").GetComponent<SpriteRenderer>().sprite =
                        checkOffImage;
                }
            }
            
            switch (type)
            {
                case  NodeType.Dialogue:
                    node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text =
                        node.bgId != BgId.Null ? node.bgId.ToString() : "";

                    node.gameObject.transform.Find("Member03").Find("InputText").GetComponent<TMP_Text>().text =
                        node.imageId != ImageId.Null ? node.imageId.ToString() : "";

                    node.gameObject.transform.Find("Member04").Find("InputText").GetComponent<TMP_Text>().text =
                        node.speaker;

                    node.gameObject.transform.Find("Member05").Find("InputText").GetComponent<TMP_Text>().text =
                        node.dialogueText;

                    if (node.isUseTextEffect)
                    {
                        node.gameObject.transform.Find("Member06").Find("InputText").GetComponent<TMP_Text>().text =
                            node.dialogueTextEffectId.ToString();
                        node.gameObject.transform.Find("Member06").Find("InputText").Find("TextBox")
                            .GetComponent<SpriteRenderer>().color = Color.white;
                        node.gameObject.transform.Find("Member06").Find("CheckBox").GetComponent<SpriteRenderer>().sprite =
                            checkOnImage;
                    }
                    else
                    {
                        node.gameObject.transform.Find("Member06").Find("InputText").GetComponent<TMP_Text>().text = "";
                        node.gameObject.transform.Find("Member06").Find("InputText").Find("TextBox")
                            .GetComponent<SpriteRenderer>().color = Color.gray;
                        node.gameObject.transform.Find("Member06").Find("CheckBox").GetComponent<SpriteRenderer>().sprite =
                            checkOffImage;
                    }
                    break;
                
                case NodeType.Selection:
                    node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text = node.selectionId;
                    break;
                
                case NodeType.SelectionText:
                    node.gameObject.transform.Find("Member01").Find("InputText").GetComponent<TMP_Text>().text = node.selectionText;
                    break;
                
                case NodeType.GetClue:
                    node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text =
                        node.clueId != Clue.Null ? node.clueId.ToString() : "";
                    break;
                
                case NodeType.GetItem:
                    node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text =
                        node.itemId != Item.Null ? node.itemId.ToString() : "";
                    break;
                
                case NodeType.Event:
                    node.gameObject.transform.Find("Member02").Find("InputText").GetComponent<TMP_Text>().text =
                        node.eventId != EventId.Null ? node.eventId.ToString() : "";
                    break;
            }
        }
    }
}
