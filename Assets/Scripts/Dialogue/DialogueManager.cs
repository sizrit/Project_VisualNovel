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

class AA
{
    private static AA aa = new AA();
    public static AA GetInstance()
    {
        return aa;
    }

    public void Hi()
    {
    }
}

class BB
{
    private AA aa;
    //시작시 단한번 불림
    void Start()
    {
        aa = AA.GetInstance();
    }
    
    // 자주는 아니지만 여러번 불림
    private void Test()
    {
        AA.GetInstance().Hi();      // 1번
        aa.Hi();                    // 2번
    }
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

    private UI_GameMenu_DialogueLogManager _uiGameMenuDialogueLogManager;

    public void OnEnable()
    {
        _speaker = GameObject.Find("Dialogue_Speaker");
        _animationManager = DialogueTextAnimationManager.GetInstance();
        _colorManager = DialogueTextColorManager.GetInstance();
        _effectManager = DialogueTextEffectManager.GetInstance();
        
        _uiGameMenuDialogueLogManager = UI_GameMenu_DialogueLogManager.GetInstance();
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
        
        _uiGameMenuDialogueLogManager.AddDialogueLog(_currentDialogue);
    }
}
