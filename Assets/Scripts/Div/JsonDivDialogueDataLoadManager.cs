using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DivJsonData
{
    public Dictionary<string,List<Dialogue>> divJsonTextData = new Dictionary<string, List<Dialogue>>();
}

public class JsonDivDialogueDataLoadManager : MonoBehaviour
{
    #region Singleton

    private static JsonDivDialogueDataLoadManager _instance;

    public static JsonDivDialogueDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<JsonDivDialogueDataLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("JsonDivDialogueLoadManager");
                _instance = gameObject.AddComponent<JsonDivDialogueDataLoadManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = GameObject.FindObjectsOfType<JsonDivDialogueDataLoadManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private readonly Dictionary<Chapter,Dictionary<string,List<Dialogue>>> _allDivDialogueList = new Dictionary<Chapter, Dictionary<string, List<Dialogue>>>();

    private LanguageType _languageType;
    
    private void LoadJsonData()
    {
        _languageType = LanguageManager.GetInstance().GetLanguageType();
        string lang = "En";
        switch (_languageType)
        {
            case LanguageType.English:
                lang = "En";
                break;
            case LanguageType.Korean:
                lang = "Ko";
                break;
        }
        
        List<Chapter> chapterList = Enum.GetValues(typeof(Chapter)).Cast<Chapter>().ToList();
        for (int i = 0; i < chapterList.Count; i++)
        {
            
            string filePath = "JsonData/"+chapterList[i].ToString()+"DivJsonTextData_"+_languageType;
            DivJsonData jsonData = LoadJsonFiles<DivJsonData>(filePath);
            Dictionary<string,List<Dialogue>> divDialogueList = jsonData.divJsonTextData;
            _allDivDialogueList.Add(chapterList[i],divDialogueList);
        }
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    { 
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }

    public List<Dialogue> GetDivDialogue(Chapter chapterValue, string divIdValue)
    {
        return _allDivDialogueList[chapterValue][divIdValue];
    }
}
