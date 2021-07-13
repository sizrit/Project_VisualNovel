using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroyBoardManager : MonoBehaviour
{
    private StroyBoard _stroyBoard;

    private void OnEnable()
    {
        _stroyBoard= new StroyBoard("s",TextID.Text01,"s","s");
    }

    private void SetNextStoryBoardId()
    {
        JsonTextDataLoadManager.GetInstance().GetTextData(_stroyBoard.GetTextId());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            string textData =  JsonTextDataLoadManager.GetInstance().GetTextData(_stroyBoard.GetTextId());
            TextManager.GetInstance().SetText(textData);
            Debug.Log(JsonTextDataLoadManager.GetInstance().GetTextData(TextID.Text01)+"sss");
        }
    }
}
