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
    private Action _dialogueTextManagerAction;

    private char[] _dialogueTextData = "".ToCharArray();
    private string _currentString = "";
    private string _pastString = "";
    private int _index = 0;

    private void OnEnable()
    {
        _speaker = this.transform.GetChild(0).gameObject;
        _dialogueText = this.transform.GetChild(0).gameObject;
    }

    public void SetDialogue(Dialogue dialogueValue)
    {
        _currentDialogue = dialogueValue;
        _speaker.GetComponent<Text>().text = _currentDialogue.speaker;
        
        _dialogueTextData = _currentDialogue.dialogueText.ToCharArray();
        //_dialogueText.GetComponent<Text>().text = _currentDialogue.dialogueText;
    }
    
    private void SetDialogueEffect()
    {
       // this.gameObject.GetComponent<DialogueTextEffectManager>().SetDialogueTextEffect();
    }


    private GameObject _currnetDialogueText;
    
    private void func0(){}
    
    private float _fadeSpeed = 0.03f;
    private void DialogueTextAnimation_FadeIn()
    {
        Color color = _dialogueText.GetComponent<Text>().color;
        color.a += _fadeSpeed;
        if (color.a > 0.95)
        {
            color.a = 1;
            _dialogueTextManagerAction = new Action(func0);
            _dialogueTextManagerAction += DialogueTextAnimation_Add;
        }
        _dialogueText.GetComponent<Text>().color = color;
    }
    
    private void DialogueTextAnimation_Add()
    {
        if (_index < _dialogueTextData.Length + 1)
        {
            _currentString += _dialogueTextData[_index];
        }
        
        if (_index > 0)
        {
            _pastString += _dialogueTextData[_index - 1];
        }

        _index++;
        if (_index == _dialogueTextData.Length)
        {
            _dialogueTextManagerAction = new Action(func0);
            
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _dialogueTextManagerAction();
    }
}
