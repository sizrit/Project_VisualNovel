using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BgLoadManager : MonoBehaviour
{
    #region Singleton

    private static BgLoadManager _instance;

    public static BgLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<BgLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("ImageLoadManager");
                _instance = gameObject.AddComponent<BgLoadManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<BgLoadManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    private readonly Dictionary<string,Sprite> _bgList = new Dictionary<string, Sprite>();
    
    private void OnEnable()
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
        this.gameObject.GetComponent<Image>().sprite = _bgList[bgIdValue];
    }
}
