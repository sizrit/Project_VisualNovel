using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoard
{
    private string storyBoardId;
    
    private Dialogue _dialogue;

    private string _bgId;

    private string _eventId;

    public StoryBoard(string storyBoardId, string bgId, string eventId, Dialogue dialogue)
    {
        this.storyBoardId = storyBoardId;
        _dialogue = dialogue;
        _bgId = bgId;
        _eventId = eventId;
    }

}
