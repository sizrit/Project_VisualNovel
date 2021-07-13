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
public enum TextID
{
    Text01,
    Text02,
}

[Serializable]
public class TextDataList
{
    public string Text01;
    public string Text02;
}

public class JsonTextDataLoadManager : MonoBehaviour
{
    private static JsonTextDataLoadManager _instance;

    public static JsonTextDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject newObj = new GameObject("JsonTextDataLoadManager");
            _instance = newObj.AddComponent<JsonTextDataLoadManager>();
        }
        return _instance;
    }
    
    private bool isInitDone = false;
    Dictionary<TextID,string> textDataList = new Dictionary<TextID, string>();

    private void Awake()
    {
        var objs = FindObjectsOfType<JsonTextDataLoadManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void OnEnable()
    {
        MakeTextList();
    }

    private void MakeTextList()
    {
        string filePath =  "JsonData/JsonTextData";
        TextDataList jsonData = LoadJsonFiles<TextDataList>(filePath);

        List<TextID> enumList = new List<TextID>();
        enumList = Enum.GetValues(typeof(TextID)).Cast<TextID>().ToList();

        FieldInfo[] f = typeof(TextDataList).GetFields(BindingFlags.Public | BindingFlags.Instance);

        for (int i = 0; i < enumList.Count; i++)
        {
            textDataList.Add(enumList[i],(string)f[i].GetValue(jsonData));
        }

        isInitDone = true;
        //Debug.Log(GetTextData(TextID.Text01));
        //Debug.Log(GetTextData(TextID.Text02));
    }

    public string GetTextData(TextID idValue)
    {
        if (isInitDone)
        {
            return textDataList[idValue];
        }
        return "";
    }
    
    
    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
