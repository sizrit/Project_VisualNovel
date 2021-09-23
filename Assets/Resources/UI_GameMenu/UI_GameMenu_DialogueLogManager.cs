using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameMenu_DialogueLogManager
{
    #region Singleton

    private static UI_GameMenu_DialogueLogManager _instance;

    public static UI_GameMenu_DialogueLogManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new UI_GameMenu_DialogueLogManager();
        }
        return _instance;
    }

    #endregion

    List<string> _dialogueList= new List<string>();

    public void ResetDialogueLog()
    {
        _dialogueList = new List<string>();
    }
    
    public void AddDialogueLog(Dialogue dialogue)
    {
        string tempString = "";
        tempString += dialogue.speaker + " : ";
        tempString += dialogue.dialogueText;
        _dialogueList.Add(tempString);
    }

    public void ShowDialogueLog()
    {
        string logString ="";
        foreach (var dialogue in _dialogueList)
        {
            logString += dialogue + "\n\n\n\n"+"우효우효우효우효"+"\n\n\n";
        }
        GameObject.Find("DialogueLogContent").GetComponent<Text>().text = logString;
        GameObject.Find("DialogueLogView").GetComponent<ScrollSystem>().SetPosition();
    }

}
