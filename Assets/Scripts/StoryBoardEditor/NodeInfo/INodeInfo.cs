using UnityEngine;

namespace StoryBoardEditor.NodeInfo
{
    public interface INodeInfo
    {
        public void SetNodeInfo(Node.Node node);
        
        public void Click(RaycastHit2D[] hits);
    }
}
