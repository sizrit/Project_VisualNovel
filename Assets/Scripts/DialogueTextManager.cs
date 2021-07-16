using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextManager : MonoBehaviour
{
    #region Singleton

    private static DialogueTextManager _instance;
    
    public static DialogueTextManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject newObj = new GameObject("DialogueTextManager");
            _instance = newObj.AddComponent<DialogueTextManager>();
        }

        return _instance;
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<DialogueTextManager>();
        if (objs.Length != 1)
        {
            Destroy(this);
            return;
        }
        _instance = this;
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

    public void SetDialogue(Dialogue dialogueValue)
    {
        _currentDialogue = dialogueValue;
        _speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        this.gameObject.GetComponent<DialogueTextColorManager>().SetDialogueTextColor(dialogueValue.color);

        //this.gameObject.GetComponent<DialogueTextEffectManager>().CheckDialogueTextEffect(dia);
        
        this.gameObject.GetComponent<DialogueTextAnimationManager>().PlayDialogueTextAnimation(_currentDialogue.dialogueText.ToCharArray());
       
    }
}
