using UnityEngine;

namespace StoryBoardEditor
{
    public class Node
    {
        public string nodeId;
        public GameObject nodeObject;
        private StoryBoard _storyBoard;

        private Node _prevNode;
        private Node _nextNode;

        private Line _outputLine;
        private Line _inputLine;

        public Node(string nodeId, GameObject nodeObject)
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

        public Node GetPrevNode()
        {
            return _prevNode;
        }

        public void SetPrevNode(Node node)
        {
            _prevNode = node;
        }
        
        public Node GetNextNode()
        {
            return _nextNode;
        }

        public void SetNextNode(Node node)
        {
            _nextNode = node;
        }

        public void SetLine(LineEdge edge, Line line)
        {
            switch (edge)
            {
                case LineEdge.Input:
                    _inputLine = line;
                    break;
                case LineEdge.Output:
                    _outputLine = line;
                    break;
            }
        }

        public Line GetLine(LineEdge edge)
        {
            switch (edge)
            {
                case LineEdge.Input:
                    return _inputLine;
                
                case LineEdge.Output:
                    return _outputLine;
            }

            return null;
        }
    }
}
