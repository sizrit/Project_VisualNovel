using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    #region Singleton

    private static GameSystem _instance;

    public static GameSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<GameSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameSystem");
                _instance = gameObject.AddComponent<GameSystem>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<GameSystem>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    // Loader
    private StoryBoardDataLoadManager _storyBoardDataLoadManager;
    private DialogueDataLoadManager _dialogueDataLoadManager;
    private StoryBoardSelectionEventDataLoadManager _storyBoardSelectionEventDataLoadManager;
    private ClueDataLoadManager _clueDataLoadManager;
    
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

    private bool _isPaused = false;
    
    private void OnEnable()
    {
        GetAllInstance();

        _storyBoardDataLoadManager.OnEnable();
        _dialogueDataLoadManager.OnEnable();
        _storyBoardSelectionEventDataLoadManager.OnEnable();
        _clueDataLoadManager.OnEnable();
        
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

    private void GetAllInstance()
    {
        _storyBoardDataLoadManager =StoryBoardDataLoadManager.GetInstance();
        _dialogueDataLoadManager = DialogueDataLoadManager.GetInstance();
        _storyBoardSelectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        _clueDataLoadManager =ClueDataLoadManager.GetInstance();
        
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

    public void PauseOn()
    {
        ClickSystem.GetInstance().DisableClick();
        _isPaused = true;
    }
    
    public void PauseOff()
    {
        ClickSystem.GetInstance().EnableClick();
        _isPaused = false;
    }
    
    private void StoryBoardUpdate()
    {
        _dialogueTextAnimationManager.Update();
        _dialogueTextEffectManager.Update();
    }
    
    void Update()
    {
        if (!_isPaused)
        {
            StoryBoardUpdate();
        }
    }
}
