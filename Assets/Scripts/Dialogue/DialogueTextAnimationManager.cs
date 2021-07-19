using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextAnimationManager : MonoBehaviour
{
    private GameObject _currentDialogueText;
    private GameObject _pastDialogueText;
    
    private Action _dialogueTextManagerAction;

    private string _dialogueTextData;
    private char[] _dialogueTextDataChar;
    private string _currentString = "";
    private string _pastString = "";
    private int _index = 0;
    
    [SerializeField]
    private float fadeSpeed = 0.1f;
    
    private bool _isAnimationEnd=true;
    
    private void func0(){}

    private void OnEnable()
    {
        _dialogueTextManagerAction= new Action(func0);
        _currentDialogueText = this.transform.GetChild(1).gameObject;
        _pastDialogueText = this.transform.GetChild(2).gameObject;
    }

    public bool GetIsAnimationEnd()
    {
        return _isAnimationEnd;
    }

    public void EndAnimationForced()
    {
        _dialogueTextManagerAction = new Action(func0);
        _currentDialogueText.GetComponent<Text>().text = _dialogueTextData;
        _pastDialogueText.GetComponent<Text>().text = _dialogueTextData;
        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 1;
        _currentDialogueText.GetComponent<Text>().color = color;
        _pastDialogueText.GetComponent<Text>().color = color;
        _isAnimationEnd = true;
    }

    public void ResetDialogueTextAnimationManager()
    {
        _dialogueTextDataChar = "".ToCharArray();
        _currentString = "";
        _pastString = "";
        _index = 0;
    }

    public void PlayDialogueTextAnimation(string dialogueTextDataValue)
    {
        _isAnimationEnd = false;
        _dialogueTextData = dialogueTextDataValue;
        _dialogueTextDataChar = dialogueTextDataValue.ToCharArray();

        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 0;
        _currentDialogueText.GetComponent<Text>().color = color;
        _dialogueTextManagerAction = new Action(func0);
        _dialogueTextManagerAction += DialogueTextAnimation_Add;
    }
    
    private void DialogueTextAnimation_FadeIn()
    {
        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a += fadeSpeed;
        if (color.a > 0.99)
        {
            color.a = 1;
            _dialogueTextManagerAction = new Action(func0);
            _dialogueTextManagerAction += DialogueTextAnimation_Add;
        }
        _currentDialogueText.GetComponent<Text>().color = color;
    }
    
    private void DialogueTextAnimation_Add()
    {
        if (_index > 0)
        {
            _pastString += _dialogueTextDataChar[_index - 1];
            _pastDialogueText.GetComponent<Text>().text = _pastString;
        }
        
        if (_index == _dialogueTextDataChar.Length)
        {
            _dialogueTextManagerAction = new Action(func0);
            _isAnimationEnd = true;
            return;
        }
        
        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 0;
        _currentDialogueText.GetComponent<Text>().color = color;
        
        if (_index < _dialogueTextDataChar.Length + 1)
        {
            _currentString += _dialogueTextDataChar[_index];
            _currentDialogueText.GetComponent<Text>().text = _currentString;
        }
        
        _dialogueTextManagerAction = new Action(func0);
        _dialogueTextManagerAction += DialogueTextAnimation_FadeIn;
        
        _index++;
    }

    private void Update()
    {
        _dialogueTextManagerAction();
    }
}
