using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BgLoadManager
{
    #region Singleton

    private static BgLoadManager _instance;

    public static BgLoadManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new BgLoadManager();
        }

        return _instance;
    }

    #endregion
    
    private readonly Dictionary<string,Sprite> _bgList = new Dictionary<string, Sprite>();
    private GameObject _bg;
    
    public void OnEnable()
    {
        LoadBg();
    }

    private void LoadBg()
    {
        string loadPath = "Bg";
        Sprite[] spriteList = Resources.LoadAll<Sprite>(loadPath);
        foreach (var sprite in spriteList)
        {
            _bgList.Add(sprite.name,sprite);
        }
    }

    public void SetBg(string bgIdValue)
    {
        GameObject.Find("StoryBoardBg").GetComponent<Image>().sprite = _bgList[bgIdValue];
    }
}
