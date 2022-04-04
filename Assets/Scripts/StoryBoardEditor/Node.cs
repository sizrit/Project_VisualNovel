using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum NodeType
    {
        Dialogue,
        Selection,
        SelectionText,
        GetClue,
        GetItem,
        Event
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Node
    {
        public NodeType type;
        
        public bool isUseStaticStoryBoardId = false;
        public string staticStoryBoardId;
        
        public string id;
        public GameObject gameObject;
        public GameObject input;
        public GameObject output;
        
        public StoryBoard storyBoard;

        public readonly List<Line> outputLineList = new List<Line>();
        public readonly List<Line> inputLineList =new List<Line>();

        public string dialogueText;
        public string speaker;

        public bool isUseTextEffect =false;
        public DialogueTextEffectId dialogueTextEffectId;

        public string selectionId;

        public string selectionText;
        
        public Clue clueId;

        public Item itemId;

        public string eventId;
    }
}
