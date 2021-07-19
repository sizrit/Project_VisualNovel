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

    private void OnEnable()
    {
        _speaker = this.transform.GetChild(0).gameObject;
        _dialogueText = this.transform.GetChild(1).gameObject;
    }

    public void SetDialogue(string dialogueIdValue)
    {
        _currentDialogue = JsonDialogueDataLoadManager.GetInstance().GetDialogue(dialogueIdValue);
        _dialogueText.GetComponent<Text>().text = "";
        this.gameObject.GetComponent<DialogueTextAnimationManager>().ResetDialogueTextAnimationManager();
        
        _speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        this.gameObject.GetComponent<DialogueTextColorManager>().SetDialogueTextColor(_currentDialogue.color);

        //this.gameObject.GetComponent<DialogueTextEffectManager>().CheckDialogueTextEffect(dia);
        this.gameObject.GetComponent<DialogueTextAnimationManager>().PlayDialogueTextAnimation(_currentDialogue.dialogueText.ToCharArray());
       
    }
}
