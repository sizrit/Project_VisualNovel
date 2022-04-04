using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardEditor.NodeInfo
{
    public class SelectionTextInfo : MonoBehaviour, INodeInfo
    {
        [SerializeField] private GameObject selectionTextInputField;

        [SerializeField] private GameObject apply;
    
    
        public void SetNodeInfo(Node node)
        {
            selectionTextInputField.GetComponent<InputField>().text = node.selectionText;
        }

        public void Click(RaycastHit2D[] hits)
        {
            Node node = NodeManipulator.GetInstance().GetSelectedNode();
            
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
            
            Node node = NodeManipulator.GetInstance().GetSelectedNode();
            node.selectionText = selectionTextInputField.GetComponent<InputField>().text;
        }
    }
}
