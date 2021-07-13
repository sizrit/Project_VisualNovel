using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroyBoard
{
    private string stroyBoardId;
    
    private TextID textId;

    private string bgId;

    private string eventId;

    public StroyBoard(string stroyBoardId, TextID textId, string bgId, string eventId)
    {
        this.stroyBoardId = stroyBoardId;
        this.textId = textId;
        this.bgId = bgId;
        this.eventId = eventId;
    }

    public TextID GetTextId()
    {
        return textId;
    }

    public string GetBgId()
    {
        return bgId;
    }

    public string GetEventId()
    {
        return eventId;
    }
}
