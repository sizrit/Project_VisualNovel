using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardNode
    {
        public string nodeId;
        public GameObject nodeObject;
        private StoryBoard _storyBoard;

        private StoryBoardNode prevStoryBoardNode;
        private StoryBoardNode nextStoryBoardNode;

        private StoryBoardEditorLine outputLine;
        private StoryBoardEditorLine inputLine;

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

        public StoryBoardNode GetPrevNode()
        {
            return prevStoryBoardNode;
        }

        public void SetPrevNode(StoryBoardNode node)
        {
            prevStoryBoardNode = node;
        }
        
        public StoryBoardNode GetNextNode()
        {
            return nextStoryBoardNode;
        }

        public void SetNextNode(StoryBoardNode node)
        {
            nextStoryBoardNode = node;
        }

        public void SetLine(LineEdge edge, StoryBoardEditorLine line)
        {
            switch (edge)
            {
                case LineEdge.Input:
                    inputLine = line;
                    break;
                case LineEdge.Output:
                    outputLine = line;
                    break;
            }
        }

        public StoryBoardEditorLine GetLine(LineEdge edge)
        {
            switch (edge)
            {
                case LineEdge.Input:
                    return inputLine;
                
                case LineEdge.Output:
                    return outputLine;
            }

            return null;
        }
    }
}
