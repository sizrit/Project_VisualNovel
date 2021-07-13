using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroyBoardManager : MonoBehaviour
{

    private void SetNextStoryBoardId()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(JsonTextDataLoadManager.GetInstance().GetTextData(TextID.Text01)+"sss");
        }
    }
}
