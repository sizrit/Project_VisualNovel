using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum NodeType
    {
        Dialogue,
        Selection,
        GainClue,
        
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Node
    {
        public NodeType type;
        
        public bool isUseStaticStoryBoardId = false;
        public string storyBoardId;
        
        public string id;
        public GameObject gameObject;
        public GameObject input;
        public GameObject output;
        
        public StoryBoard storyBoard;

        public readonly List<Line> outputLineList = new List<Line>();
        public readonly List<Line> inputLineList =new List<Line>();

    }
}
