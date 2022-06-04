using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DialogueSystem;
using StoryBoardSystem;
using UnityEngine;

namespace StoryBoardEditor.Node_ScriptAsset
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

        public readonly List<Line_ScriptAsset.Line> outputLineList = new List<Line_ScriptAsset.Line>();
        public readonly List<Line_ScriptAsset.Line> inputLineList =new List<Line_ScriptAsset.Line>();

        public BgId bgId;
        public ImageId imageId;
        public string dialogueText;
        public string speaker;

        public bool isUseTextEffect =false;
        public DialogueTextEffectId dialogueTextEffectId;

        public string selectionId;

        public string selectionText;
        
        public Clue clueId;

        public Item itemId;

        public EventId eventId;
    }
}
