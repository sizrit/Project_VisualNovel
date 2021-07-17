using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private List<Dialogue> _mainDialogueList= new List<Dialogue>();
    
    private List<Dialogue> _currentDialogueList = new List<Dialogue>();
    private readonly Dictionary<string, Dialogue> _currentDialogueDictionary = new Dictionary<string, Dialogue>();
    
    //private DivInfo _divInfo =new DivInfo();

    private Dialogue _currentDialogue;

    private void MakeCurrentDialogueDictionary(List<Dialogue> dialogueListValue)
    {
        foreach (var dialogue in dialogueListValue)
        {
            _currentDialogueDictionary.Add(dialogue.dialogueId,dialogue);
        }
    }
    
    public void SetMainDialogue(Chapter chapterValue)
    {
        _mainDialogueList = JsonDialogueDataLoadManager.GetInstance().GetDialogue(chapterValue);
    }
    
    public void SetCurrentDialogueList(string divIdValue, Chapter chapterValue, string dialogueIdValue)
    {
        if (divIdValue == "Main")
        {
            _currentDialogueList = _mainDialogueList;
        }
        else
        {
            _currentDialogueList =
                JsonDivDialogueDataLoadManager.GetInstance().GetDivDialogue(chapterValue, divIdValue);
        }
        
        MakeCurrentDialogueDictionary(_currentDialogueList);
        
        if (dialogueIdValue == "")
        {
            _currentDialogue = _currentDialogueList[0];
        }
        else
        {
            _currentDialogue = _currentDialogueDictionary[dialogueIdValue];
        }
    }

    public void SetNextDialogue()
    {
        int currentIndex = _currentDialogueList.IndexOf(_currentDialogue) + 1;
        _currentDialogue = _currentDialogueList[currentIndex];
    }

    public void ShowDialogue()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
