using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoryBoard
{
    private string _storyBoardId;
    
    private string _dialogueId;

    private string _bgId;

    private string _eventId;

    private string _nextStoryBoardId;

    public StoryBoard()
    {
        
    }
    
    public StoryBoard(string storyBoardId, string bgId, string eventId, string dialogueId)
    {
        _storyBoardId = storyBoardId;
        _dialogueId = dialogueId;
        _bgId = bgId;
        _eventId = eventId;
    }

    public string GetStroyBoardId()
    {
        return _storyBoardId;
    }
    
    public string GetDialogueId()
    {
        return _dialogueId;
    }
    
    public string GetBgId()
    {
        return _bgId;
    }

    public string GetNextStoryBoardId()
    {
        return _nextStoryBoardId;
    }
    

}
