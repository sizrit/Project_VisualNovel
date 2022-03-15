using UnityEngine;

namespace StoryBoardEditor
{
    public class Node
    {
        public string id;
        public GameObject gameObject;
        public GameObject input;
        public GameObject output;
        
        public StoryBoard storyBoard;

        public Node prevNode;
        public Node nextNode;

        public Line outputLine;
        public Line inputLine;

        public Line GetLine(LineEdge edge)
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
