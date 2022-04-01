using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum BgId
{
    Null,
    Chapter01Room,
    
    
    a,
    aa,
    aaa,
    aaaa,
    aaaaa
}

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
    
    private readonly Dictionary<BgId,Sprite> _bgList = new Dictionary<BgId, Sprite>();
    [SerializeField] private GameObject bg;
    
    public void LoadBg()
    {
        List<BgId> bgNameList = Enum.GetValues(typeof(BgId)).Cast<BgId>().ToList();
        string loadPath = "Bg";
        Sprite[] spriteList = Resources.LoadAll<Sprite>(loadPath);

        foreach (var sprite in spriteList)
        {
            foreach (var bgName in bgNameList)
            {
                if (bgName.ToString() == sprite.name)
                {
                    _bgList.Add(bgName,sprite);
                }
            }
        }
    }

    public void SetBg(BgId bgId)
    {
        bg.GetComponent<Image>().sprite = _bgList[bgId];
    }
    
    public static BgId ConvertToBgId(string stringValue)
    {
        List<BgId> bgIdList = Enum.GetValues(typeof(BgId)).Cast<BgId>().ToList();

        foreach (var bgId in bgIdList)
        {
            if (stringValue == bgId.ToString())
            {
                return bgId;
            }
        }

        return BgId.Null;
    }
}
