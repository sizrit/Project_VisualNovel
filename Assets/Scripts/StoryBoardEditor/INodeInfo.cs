using UnityEngine;

namespace StoryBoardEditor
{
    public interface INodeInfo
    {
        public void SetNodeInfo(Node node);
        
        public void Click(RaycastHit2D[] hits);
    }
}
