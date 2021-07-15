using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    English,
    Korean
}

public class LanguageManager 
{
    #region Singleton

    private static LanguageManager _instance;

    public static LanguageManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new LanguageManager();
        }
        return _instance;
    }
    
    #endregion

    private LanguageType _languageType = LanguageType.English;

    public void SetLanguageType(LanguageType type)
    {
        _languageType = type;
    }
    
    public LanguageType GetLanguageType()
    {
        return _languageType;
    }
}
