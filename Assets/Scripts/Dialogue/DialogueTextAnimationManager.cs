using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextAnimationManager
{
    #region Singleton

    private static DialogueTextAnimationManager _instance;
    
    public static DialogueTextAnimationManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DialogueTextAnimationManager();
        }

        return _instance;
    }

    #endregion
    
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

    public void OnEnable()
    {
        _dialogueTextManagerAction= new Action(func0);
        _currentDialogueText = GameObject.Find("Dialogue_CurrentText");
        _pastDialogueText = GameObject.Find("Dialogue_PastText");
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

    private bool _isRichText = false;
    private bool _isRichTextOn = false;
    private float timer = 0;
    
    private void DialogueTextAnimation_Add()
    {
        timer += Time.deltaTime;
        if (timer > 0.005)
        {
            timer = 0;
        }
        else
        {
            return;
        }
        
        if (_index == _dialogueTextDataChar.Length)
        {
            _dialogueTextManagerAction = new Action(func0);
            _isAnimationEnd = true;
            DialogueManager.GetInstance().AnimationEnd();
            return;
        }
        
        if (_isRichText)
        {
            _pastString+=_dialogueTextDataChar[_index - 1];
            _currentString += _dialogueTextDataChar[_index];
            if (_dialogueTextDataChar[_index] == '>')
            {
                _index++;
                _pastString+=_dialogueTextDataChar[_index - 1];
                _currentString += _dialogueTextDataChar[_index];
                
                _isRichText = false;

                if (!_isRichTextOn)
                {
                    _currentDialogueText.GetComponent<Text>().text = _currentString;
                    _pastDialogueText.GetComponent<Text>().text = _pastString;
                }
                else
                {
                    _currentDialogueText.GetComponent<Text>().text = _currentString+"</color>";
                    _pastDialogueText.GetComponent<Text>().text = _pastString+"</color>";
                }

                _dialogueTextManagerAction = new Action(func0);
                _dialogueTextManagerAction += DialogueTextAnimation_FadeIn;
                _index++;
                return;
            }
            else
            {
                _index++;
                return;
            }
        }

        if (_dialogueTextDataChar.Length != 0)
        {
            if (_dialogueTextDataChar[_index]=='<')
            {
                if (_dialogueTextDataChar[_index + 1] == '/')
                {
                    _isRichTextOn = false;
                }
                else
                {
                    _isRichTextOn = true;
                }
                _isRichText = true;
                return;
            }
        }

        if (_index > 0)
        {
            _pastString += _dialogueTextDataChar[_index - 1];
            if (!_isRichTextOn)
            {
                _pastDialogueText.GetComponent<Text>().text = _pastString;
            }
            else
            {
                _pastDialogueText.GetComponent<Text>().text = _pastString+"</color>";
            }
        }

        Color color = _currentDialogueText.GetComponent<Text>().color;
        color.a = 0;
        _currentDialogueText.GetComponent<Text>().color = color;
        
        if (_index < _dialogueTextDataChar.Length + 1)
        {
            _currentString += _dialogueTextDataChar[_index];
            if (!_isRichTextOn)
            {
                _currentDialogueText.GetComponent<Text>().text = _currentString;
            }
            else
            {
                _currentDialogueText.GetComponent<Text>().text = _currentString+"</color>";
            }
            
        }
        
        _dialogueTextManagerAction = new Action(func0);
        _dialogueTextManagerAction += DialogueTextAnimation_FadeIn;
        
        _index++;
    }

    public void Update()
    {
        _dialogueTextManagerAction();
    }
}
