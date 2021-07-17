using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct DivInfo
{
    public string divId;
    public string startDialogueId;
    public string endDialogueId;
    public string nextDivId;
    public string nextDialogueId;
}

[Serializable]
public class DivInfoData
{
    public List<DivInfo> divInfoData;
}

public class JsonDivInfoDataLoadManager : MonoBehaviour
{
    #region Singleton

    private static JsonDivInfoDataLoadManager _instance;

    public static JsonDivInfoDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<JsonDivInfoDataLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("JsonDivInfoDataLoadManager");
                _instance = gameObject.AddComponent<JsonDivInfoDataLoadManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = GameObject.FindObjectsOfType<JsonDivInfoDataLoadManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private readonly Dictionary<Chapter,List<DivInfo>> _divInfoList = new Dictionary<Chapter, List<DivInfo>>();
    
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
    
    private void LoadDivInfoList()
    {
        List<Chapter> chapterList = Enum.GetValues(typeof(Chapter)).Cast<Chapter>().ToList();
        foreach (var chapter in chapterList)
        {
            string loadPath = "JsonData/Div/"+chapter.ToString()+"DivInfoData";
            DivInfoData jsonData = LoadJsonFiles<DivInfoData>(loadPath);
            _divInfoList.Add(chapter,jsonData.divInfoData);
        }
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }

    private void OnEnable()
    {
        LoadDivInfoList();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(GetDivInfo(Chapter.Chapter01, "aa1").startDialogueId); ;
        }
    }
}
