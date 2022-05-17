using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public struct DivDialogue
{
    public string divId;
    public string dialogueId;
    public string speaker;
    public string dialogueText;
    public string color;
}

[Serializable]
public class DivJsonData
{
    public List<DivDialogue> divJsonDialogueData = new List<DivDialogue>();
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

    private void temptemp()
    {
        // List<Dialogue> dialogues = new List<Dialogue>();
        // Dialogue dialogue = new Dialogue();
        // dialogue.dialogueId = "AA";
        // dialogues.Add(dialogue);
        // DivJsonData divJsonData = new DivJsonData();
        // Debug.Log(divJsonData.divJsonDialogueData);
        // divJsonData.divJsonDialogueData.Add("AA",dialogues);
        // Debug.Log(JsonConvert.SerializeObject(divJsonData,Formatting.Indented));
    }
    
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
            string filePath = "JsonData/"+chapterList[i].ToString()+"DivDialogueData_"+lang;
            DivJsonData jsonData = LoadJsonFiles<DivJsonData>(filePath);
            List<DivDialogue> divDialogueList = jsonData.divJsonDialogueData;
            _allDivDialogueList.Add(chapterList[i],MakeDictionary(divDialogueList,chapterList[i]));
        }
    }

    private Dictionary<string,List<Dialogue>> MakeDictionary(List<DivDialogue> divDialogueListValue, Chapter chapterValue)
    {
        List<string> divIdList = JsonDivInfoDataLoadManager.GetInstance().GetDivIdList(chapterValue);
        Dictionary<string,List<Dialogue>> dialogueDictionary =new Dictionary<string, List<Dialogue>>();

        foreach (var divId in divIdList)
        {
            List<Dialogue> newDialogueList= new List<Dialogue>();
            foreach (var divDialogue in divDialogueListValue)
            {
                if (divDialogue.divId == divId)
                {
                    Dialogue newDialogue = new Dialogue();
                    newDialogue.color = divDialogue.color;
                    newDialogue.speaker = divDialogue.speaker;
                    newDialogue.storyBoardId = divDialogue.dialogueId;
                    newDialogue.dialogueText = divDialogue.dialogueText;
                    newDialogueList.Add(newDialogue);
                }
                
                if (divDialogue.Equals(divDialogueListValue[divDialogueListValue.Count - 1]))
                {
                    dialogueDictionary.Add(divId,newDialogueList);
                }
            }
        }
        Debug.Log(dialogueDictionary.Count);
        return dialogueDictionary;
        
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    { 
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }

    public List<Dialogue> GetDivDialogue(Chapter chapterValue, string divIdValue)
    {
        //Debug.Log(_allDivDialogueList[chapterValue].Count);
        return _allDivDialogueList[chapterValue][divIdValue];
    }

    private void OnEnable()
    {
        temptemp();
        LoadJsonData();
    }
}
