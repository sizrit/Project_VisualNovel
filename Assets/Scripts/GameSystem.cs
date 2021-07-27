using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    // Loader
    private StoryBoardDataLoadManager _storyBoardDataLoadManager;
    private DialogueDataLoadManager _dialogueDataLoadManager;
    private StoryBoardSelectionEventDataLoadManager _storyBoardSelectionEventDataLoadManager;
    
    // Loader & Manager
    private BgLoadManager _bgLoadManager;
    
    // Manager
    private StoryBoardManager _storyBoardManager;
    private StoryBoardEventManager _storyBoardEventManager;
    private DialogueManager _dialogueManager;
    private DialogueTextAnimationManager _dialogueTextAnimationManager;
    private DialogueTextEffectManager _dialogueTextEffectManager;
    private DialogueTextColorManager _dialogueTextColorManager;
    private ClueManager _clueManager;
    private StoryBoardGettingClueEventManager _storyBoardGettingClueEventManager;
    
    private void OnEnable()
    {
        GetInstance();

        _storyBoardDataLoadManager.OnEnable();
        _dialogueDataLoadManager.OnEnable();
        _storyBoardSelectionEventDataLoadManager.OnEnable();
        
        _bgLoadManager.OnEnable();
        
        _storyBoardManager.OnEnable();
        _storyBoardEventManager.OnEnable();
        _dialogueManager.OnEnable();
        _dialogueTextAnimationManager.OnEnable();
        _dialogueTextEffectManager.OnEnable();
        _dialogueTextColorManager.OnEnable();
        _clueManager.OnEnable();
        _storyBoardGettingClueEventManager.OnEnable();
    }

    private void GetInstance()
    {
        _storyBoardDataLoadManager =StoryBoardDataLoadManager.GetInstance();
        _dialogueDataLoadManager = DialogueDataLoadManager.GetInstance();
        _storyBoardSelectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        
        _bgLoadManager = BgLoadManager.GetInstance();
        
        _storyBoardManager = StoryBoardManager.GetInstance();
        _storyBoardEventManager = StoryBoardEventManager.GetInstance();
        _dialogueManager = DialogueManager.GetInstance();
        _dialogueTextAnimationManager = DialogueTextAnimationManager.GetInstance();
        _dialogueTextEffectManager = DialogueTextEffectManager.GetInstance();
        _dialogueTextColorManager = DialogueTextColorManager.GetInstance();
        _clueManager = ClueManager.GetInstance();
        _storyBoardGettingClueEventManager = StoryBoardGettingClueEventManager.GetInstance();
    }

    private void StoryBoardUpdate()
    {
        _dialogueTextAnimationManager.Update();
        _dialogueTextEffectManager.Update();
    }
    
    void Update()
    {
        StoryBoardUpdate();
    }
}
