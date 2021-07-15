using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueTextColor
{
    White,
    Red,
    Black
}

public enum DialogueTextEffect
{
    
}

public class DialogueTextEffectManager : MonoBehaviour
{
    private Dialogue _currentDialogue;
    private GameObject _dialogueText;

    private void OnEnable()
    {
        _dialogueText = this.transform.GetChild(1).gameObject;
    }

    public void SetDialogueTextEffect(Dialogue dialogueValue)
    {
        _currentDialogue = dialogueValue;
        SetDialogueTextColor(_currentDialogue.color);
    }

    private void SetDialogueTextColor(string colorValue)
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

        _dialogueText.GetComponent<Text>().color = color;
    }
    
}
