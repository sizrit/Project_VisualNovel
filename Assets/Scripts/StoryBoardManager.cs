using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardManager : MonoBehaviour
{
    private StoryBoard _storyBoard;
    private int _dialogueNum = 0;

    private void OnEnable()
    {
        
    }

    private void SetNextStoryBoardId()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Dialogue dialogue = JsonDialogueDataLoadManager.GetInstance().GetDialogue(Chapter.Chapter01, _dialogueNum++);
            DialogueTextManager.GetInstance().SetDialogue(dialogue);
        }
    }
}
