using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class TextDataList
{
    public List<Dialogue> dialogueList;
}

public class JsonDialogueDataLoadManager : MonoBehaviour
{
    #region Singleton

    private static JsonDialogueDataLoadManager _instance;

    public static JsonDialogueDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<JsonDialogueDataLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject newObj = new GameObject("JsonTextDataLoadManager");
                _instance = newObj.AddComponent<JsonDialogueDataLoadManager>();
            }
        }
        return _instance;
    }
    
    private void Awake()
    {
        var objs = FindObjectsOfType<JsonDialogueDataLoadManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private LanguageType _languageType;
    
    private bool _isLoadDone = false;

    private readonly Dictionary<string, Dialogue> _dialogueList = new Dictionary<string, Dialogue>();

    public Dialogue GetDialogue(string storyBoardIdValue)
    {
        if (!_isLoadDone)
        {
            LoadJsonData();
        }
        return _dialogueList[storyBoardIdValue];
    }

    private void OnEnable()
    {
        _languageType = LanguageManager.GetInstance().GetLanguageType();
        LoadJsonData();
    }

    private void LoadJsonData()
    {
        var chapterList = Enum.GetValues(typeof(Chapter)).Cast<Chapter>().ToList();

        for (int i = 1; i < chapterList.Count + 1; i++)
        {
            string filePath = "JsonData/Chapter0"+i.ToString()+"DialogueData";
            switch (_languageType)
            {
                case LanguageType.English:
                    filePath += "_En";
                    break;
                
                case LanguageType.Korean:
                    filePath += "_Ko";
                    break;
            }
            
            TextDataList jsonData = LoadJsonFiles<TextDataList>(filePath);
            MakeDictionary(jsonData.dialogueList);
        }
        _isLoadDone = true;
    }

    private void MakeDictionary(List<Dialogue> dialogueListValue)
    {
        foreach (var dialogue in dialogueListValue)
        {
            _dialogueList.Add(dialogue.storyBoardId,dialogue);
        }
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
