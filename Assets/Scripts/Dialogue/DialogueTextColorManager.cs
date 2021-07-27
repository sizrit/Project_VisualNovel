using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextColorManager
{
    #region Singleton

    private static DialogueTextColorManager _instance;
    
    public static DialogueTextColorManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DialogueTextColorManager();
        }

        return _instance;
    }

    #endregion
    
    private GameObject _currentDialogueText;
    private GameObject _pastDialogueText;

    private void OnEnable()
    {
        _currentDialogueText = GameObject.Find("Dialogue_CurrentText");
        _pastDialogueText = GameObject.Find("Dialogue_PastText");
    }

    public void SetDialogueTextColor(string colorValue)
    {
        Color color = Color.white;
        switch (colorValue)
        {
            case "White":
                color=Color.white;
                break;
            
            case "Red":
                color =Color.red;
                break;
            
            case "Black":
                color = Color.black;
                break;
        }

        _currentDialogueText.GetComponent<Text>().color = color;
        _pastDialogueText.GetComponent<Text>().color = color;
    }
}
