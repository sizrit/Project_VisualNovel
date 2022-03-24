using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum NodeType
    {
        Dialogue,
        Selection,
        GainClue,
        
    }
    
    public class Node
    {
        public NodeType type;
        
        public string id;
        public GameObject gameObject;
        public GameObject input;
        public GameObject output;
        
        public StoryBoard storyBoard;

        public readonly List<Line> outputLineList = new List<Line>();
        public readonly List<Line> inputLineList =new List<Line>();

    }
}
