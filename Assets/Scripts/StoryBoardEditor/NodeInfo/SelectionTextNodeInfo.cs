using StoryBoardEditor.Node;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    public class SelectionTextNodeInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject selectionTextInputField;

        [SerializeField] private GameObject apply;
    
    
        public void SetNodeInfo(Node.Node node)
        {
            selectionTextInputField.GetComponent<InputField>().text = node.selectionText;
        }

        public void Click(RaycastHit2D[] hits)
        {
            Node.Node node = NodeManipulator.GetInstance().GetSelectedNode();
            
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject == apply)
                {
                    ApplyData();
                }
            }
        }
        
        private void ApplyData()
        {
            Debug.Log("apply");
            
            Node.Node node = NodeManipulator.GetInstance().GetSelectedNode();
            node.selectionText = selectionTextInputField.GetComponent<InputField>().text;

            NodeVisualizeSettingManager.GetInstance().SetNode(node);
        }
    }
}
