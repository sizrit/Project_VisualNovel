using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct Dialogue
{
    public string storyBoardId;
    public string speaker;
    public string dialogueText;
    public string color;
}

public enum Chapter
{
    Chapter01,
    /*
    Chapter02,
    Chapter03,
    Chapter04,
    Chapter05
    */
}

public class DialogueManager
{
    #region Singleton

    private static DialogueManager _instance;
    
    public static DialogueManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DialogueManager();
        }

        return _instance;
    }

    #endregion
    
    private GameObject _speaker;
    private Dialogue _currentDialogue;

    private DialogueTextAnimationManager _animationManager;
    private DialogueTextColorManager _colorManager;
    private DialogueTextEffectManager _effectManager;

    private DialogueLogManager _dialogueLogManager;

    public void OnEnable()
    {
        _speaker = GameObject.Find("Dialogue_Speaker");
        _animationManager = DialogueTextAnimationManager.GetInstance();
        _colorManager = DialogueTextColorManager.GetInstance();
        _effectManager = DialogueTextEffectManager.GetInstance();
        
        _dialogueLogManager = DialogueLogManager.GetInstance();
    }

    public bool CheckIsAnimationEnd()
    {
        return _animationManager.GetIsAnimationEnd();
    }

    public void EndAnimationForced()
    {
        _animationManager.EndAnimationForced();
        _effectManager.EndEffect();
    }

    public void AnimationEnd()
    {
        _effectManager.EndEffect();
    }
    
    public void SetDialogue(string storyBoardIdValue)
    {
        _currentDialogue = DialogueDataLoadManager.GetInstance().GetDialogue(storyBoardIdValue);

        _speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        _animationManager.ResetDialogueTextAnimationManager();
        _animationManager.PlayDialogueTextAnimation(_currentDialogue.dialogueText);
        
        _effectManager.SetDialogueTextEffect(_currentDialogue.storyBoardId);
         
        _colorManager.SetDialogueTextColor(_currentDialogue.color);
        
        _dialogueLogManager.AddDialogueLog(_currentDialogue);
    }
}
