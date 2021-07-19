using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextEffectManager : MonoBehaviour
{
    private readonly Dictionary<Chapter,List<int>> _dialogueTextEffectList = new Dictionary<Chapter, List<int>>();

    private void MakeDialogueTextEffectList()
    {
        List<Chapter> chapterList = Enum.GetValues(typeof(Chapter)).Cast<Chapter>().ToList();
        foreach (var chapter in chapterList)
        {
            _dialogueTextEffectList.Add(chapter,new List<int>());
        }
    }
    
    public void SetDialogueTextEffect(string dialogueIdValue)
    {
        
    }

    public void EndEffect()
    {
        
    }
}
