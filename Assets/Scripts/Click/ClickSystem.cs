using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    private StoryBoardClickSystem _storyBoardClickSystem;
    
    
    private void OnEnable()
    {
         _storyBoardClickSystem = StoryBoardClickSystem.GetInstance();
    }

    public void SetStoryBoardMode()
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
