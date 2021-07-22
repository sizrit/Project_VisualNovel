using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct Dialogue
{
    public string dialogueId;
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

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    private static DialogueManager _instance;
    
    public static DialogueManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<DialogueManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject newObj = new GameObject("DialogueManager");
                _instance = newObj.AddComponent<DialogueManager>();
            }

        }

        return _instance;
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<DialogueManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private GameObject _speaker;
    private GameObject _dialogueText;
    private Dialogue _currentDialogue;

    private DialogueTextAnimationManager _animationManager;
    private DialogueTextColorManager _colorManager;
    private DialogueTextEffectManager _effectManager;

    private void OnEnable()
    {
        _speaker = this.transform.GetChild(0).gameObject;
        _dialogueText = this.transform.GetChild(1).gameObject;
        _animationManager = this.gameObject.GetComponent<DialogueTextAnimationManager>();
        _colorManager = this.gameObject.GetComponent<DialogueTextColorManager>();
        _effectManager = this.gameObject.GetComponent<DialogueTextEffectManager>();
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
    
    public void SetDialogue(string dialogueIdValue)
    {
        _currentDialogue = JsonDialogueDataLoadManager.GetInstance().GetDialogue(dialogueIdValue);

        _speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        _animationManager.ResetDialogueTextAnimationManager();
        _animationManager.PlayDialogueTextAnimation(_currentDialogue.dialogueText);
        
        _effectManager.SetDialogueTextEffect(_currentDialogue.dialogueId);
         
        _colorManager.SetDialogueTextColor(_currentDialogue.color);
    }
}
