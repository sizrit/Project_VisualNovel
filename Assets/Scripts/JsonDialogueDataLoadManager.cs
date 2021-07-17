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
public struct Dialogue
{
    public string dialogueId;
    public string speaker;
    public string dialogueText;
    public string color;
}

public enum Chapter
{
    Chapter01,
    /*
    Chapter02,
    Chapter03,
    Chapter04,
    Chapter05
    */
}

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

    private readonly Dictionary<Chapter, List<Dialogue>> _dialogueList = new Dictionary<Chapter, List<Dialogue>>();

    public List<Dialogue> GetDialogue(Chapter chapter)
    {
        if (_isLoadDone)
        {
            return _dialogueList[chapter];
        }
        
        Debug.Log("JsonTextDataLoadManager's Load is not Done!");
        return new List<Dialogue>();
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
            _dialogueList.Add(chapterList[i-1],jsonData.dialogueList);
        }

        _isLoadDone = true;

        /*
        List<TextId> enumList = new List<TextId>();
        enumList = Enum.GetValues(typeof(TextId)).Cast<TextId>().ToList();

        FieldInfo[] f = typeof(TextDataList).GetFields(BindingFlags.Public | BindingFlags.Instance);

        for (int i = 0; i < enumList.Count; i++)
        {
            textDataList.Add(enumList[i],(string)f[i].GetValue(jsonData));
        }

        isInitDone = true;
        //Debug.Log(GetTextData(TextID.Text01));
        //Debug.Log(GetTextData(TextID.Text02));
        */
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
