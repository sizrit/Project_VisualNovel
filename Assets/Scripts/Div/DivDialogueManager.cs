using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

/*
public class DivDialogueManager : MonoBehaviour
{
    #region Singleton
    //
    // private static DivDialogueManager _instance;
    //
    // public static DivDialogueManager GetInstance()
    // {
    //     if (_instance == null)
    //     {
    //         GameObject gameObject = new GameObject("DivDialogueManager");
    //         _instance = gameObject.AddComponent<DivDialogueManager>();
    //     }
    //     return _instance;
    // }
    //
    // private void Awake()
    // {
    //     var obj = GameObject.FindObjectsOfType<DivDialogueManager>();
    //     if (obj.Length != 1)
    //     {
    //         Destroy(this);
    //     }
    //
    //     _instance = this;
    // }

    #endregion
    
    private Dictionary<Chapter, List<DivInfo>> _divInfoList = new Dictionary<Chapter, List<DivInfo>>();

    public DivInfo GetDivInfo(Chapter chapterValue, string divIdValue)
    {
        return CheckDivInfo(divIdValue, _divInfoList[chapterValue]);
    }

    private DivInfo CheckDivInfo(string divIdValue, List<DivInfo> divInfoListValue)
    {
        foreach (var divInfo in divInfoListValue)
        {
            if (divIdValue == divInfo.divId)
            {
                return divInfo;
            }
        }

        Debug.Log("DivInfo Not Exist!");
        return new DivInfo();
    }

    private void MakeDivInfoList()
    {
        _divInfoList= JsonDivInfoDataLoadManager.GetInstance().LoadDivInfoList();
    }

    private void OnEnable()
    {
        MakeDivInfoList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(GetDivInfo(Chapter.Chapter01, "aa1").startDialogueId); ;
        }
    }
}
*/