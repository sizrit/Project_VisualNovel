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

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    private static DialogueManager _instance;
    
    public static DialogueManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<DialogueManager>();
            if (obj == null)
            {
                Debug.Log("Error! DialogueManager is disable now");
                return null;
                // GameObject gameObject = new GameObject("Dialogue");
                // _instance = gameObject.AddComponent<DialogueManager>();
            }
            else
            {
                _instance = obj;
            }
        }

        return _instance;
    }

    #endregion

    [SerializeField] private GameObject speaker;
    private Dialogue _currentDialogue;

    public bool CheckIsAnimationEnd()
    {
        return DialogueTextAnimationManager.GetInstance().GetIsAnimationEnd();
    }

    public void EndAnimationForced()
    {
        DialogueTextAnimationManager.GetInstance().EndAnimationForced();
        DialogueTextEffectManager.GetInstance().EndEffect();
    }

    public void AnimationEnd()
    {
        DialogueTextEffectManager.GetInstance().EndEffect();
    }
    
    public void SetDialogue(string storyBoardIdValue)
    {
        _currentDialogue = DialogueDataLoadManager.GetInstance().GetDialogue(storyBoardIdValue);

        Debug.Log(speaker);
        speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        DialogueTextAnimationManager.GetInstance().ResetDialogueTextAnimationManager();
        DialogueTextAnimationManager.GetInstance().PlayDialogueTextAnimation(_currentDialogue.dialogueText);

        DialogueTextEffectManager.GetInstance().SetDialogueTextEffect(_currentDialogue.storyBoardId);
         
        DialogueTextColorManager.GetInstance().SetDialogueTextColor(_currentDialogue.color);
        
        UI_GameMenu_DialogueLogManager.GetInstance().AddDialogueLog(_currentDialogue);
    }
}
