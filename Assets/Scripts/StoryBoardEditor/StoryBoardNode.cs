using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardNode
    {
        public string nodeId;
        public GameObject nodeObject;
        private StoryBoard _storyBoard;

        public StoryBoardNode prevStoryBoardNode;
        public StoryBoardNode nextStoryBoardNode;

        public StoryBoardNode(string nodeId, GameObject nodeObject)
        {
            this.nodeId = nodeId;
            this.nodeObject = nodeObject;
        }

        public void SetStoryBoard(StoryBoard storyBoard)
        {
            this._storyBoard = storyBoard;
        }

        public StoryBoard GetStoryBoard()
        {
            return _storyBoard;
        }
    }
}
