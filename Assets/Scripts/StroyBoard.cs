using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroyBoard
{
    private string stroyBoardId;
    
    private Dialogue _dialogue;

    private string _bgId;

    private string _eventId;

    public StroyBoard(string stroyBoardId, string bgId, string eventId, Dialogue dialogue)
    {
        this.stroyBoardId = stroyBoardId;
        _dialogue = dialogue;
        _bgId = bgId;
        _eventId = eventId;
    }

}
