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

    private char[] _dialogueTextData;
    private string _currentString = "";
    private string _pastString = "";
    private int _index = 0;
    
    private float _fadeSpeed = 0.06f;
    
    private void func0(){}

    private void OnEnable()
    {
        _dialogueTextManagerAction= new Action(func0);
        _currentDialogueText = this.transform.GetChild(1).gameObject;
        _pastDialogueText = this.transform.GetChild(2).gameObject;
    }

    public void ResetDialogueTextAnimationManager()
    {
        _dialogueTextData = "".ToCharArray();
        _currentString = "";
        _pastString = "";
        _index = 0;
    }

    public void PlayDialogueTextAnimation(char[] dialogueTextDataValue)
    {
        _dialogueTextData = dialogueTextDataValue;

        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 0;
        _currentDialogueText.GetComponent<Text>().color = color;
        _dialogueTextManagerAction = new Action(func0);
        _dialogueTextManagerAction += DialogueTextAnimation_Add;
    }
    
    
    private void DialogueTextAnimation_FadeIn()
    {
        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a += _fadeSpeed;
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
            _pastString += _dialogueTextData[_index - 1];
            _pastDialogueText.GetComponent<Text>().text = _pastString;
        }
        
        if (_index == _dialogueTextData.Length)
        {
            _dialogueTextManagerAction = new Action(func0);
            return;
        }
        
        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 0;
        _currentDialogueText.GetComponent<Text>().color = color;
        
        if (_index < _dialogueTextData.Length + 1)
        {
            _currentString += _dialogueTextData[_index];
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
