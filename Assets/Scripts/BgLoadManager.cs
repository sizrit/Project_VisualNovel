using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BgId
{
    bg01,
    bg02
}

public class BgLoadManager : MonoBehaviour
{
    private List<BgId> _bgIdList = new List<BgId>();
    Dictionary<BgId,Sprite> _spriteList = new Dictionary<BgId, Sprite>();
    private void OnEnable()
    {
        _bgIdList = Enum.GetValues(typeof(BgId)).Cast<BgId>().ToList();
        string loadPath = "BG/";
        for(int i=0; i< _bgIdList.Count; i++)
        {
            _spriteList.Add(_bgIdList[i],Resources.Load<Sprite>(loadPath+_bgIdList[i].ToString()));
        }
    }

    public void LoadBg(BgId bgIdValue)
    {
        this.GetComponent<SpriteRenderer>().sprite = _spriteList[bgIdValue];
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.GetComponent<SpriteRenderer>().sprite = _spriteList[BgId.bg01];
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<SpriteRenderer>().sprite = _spriteList[BgId.bg02];
        }
        
    }
}
