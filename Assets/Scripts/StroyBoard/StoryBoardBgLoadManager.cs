using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardBgLoadManager : MonoBehaviour
{
    #region Singleton

    private static StoryBoardBgLoadManager _instance;

    public static StoryBoardBgLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardBgLoadManager>();
            if (obj == null)
            {
                Debug.LogError("Error! StoryBoardBgLoadManager is disable now");
                return null;
            }
            _instance = obj;
        }

        return _instance;
    }

    #endregion
    
    private readonly Dictionary<string,Sprite> _bgList = new Dictionary<string, Sprite>();
    [SerializeField] private GameObject bg;
    
    public void LoadBg()
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
        bg.GetComponent<Image>().sprite = _bgList[bgIdValue];
    }
}
