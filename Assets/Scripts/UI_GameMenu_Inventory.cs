using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameMenu_Inventory : MonoBehaviour
{
    private Dictionary<string, Clue> _currentClueList = new Dictionary<string, Clue>();
    private Dictionary<string,Sprite> _clueIconImageList = new Dictionary<string, Sprite>();

    private GameObject _clueIconPrefabs;

    private GameObject _detail;

    private void OnEnable()
    {
        _detail = GameObject.Find("Detail");
        
        ClueManager.GetInstance().GetClue("Clue01");
        ClueManager.GetInstance().GetClue("Clue02");

        _currentClueList = ClueManager.GetInstance().GetCurrentClueList();
        LoadPrefabs();
        LoadImages();
        ShowClue();
        
        UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
    }

    private void LoadPrefabs()
    {
        string loadPath = "Clue/Prefabs/ClueIcon";
        _clueIconPrefabs = Resources.Load<GameObject>(loadPath);
    }

    private void LoadImages()
    {
        string loadPath = "Clue/Images/";
        foreach (var clue in _currentClueList)
        {
            if (!_clueIconImageList.ContainsKey(clue.Value.id))
            {
                _clueIconImageList.Add(clue.Value.id,Resources.Load<Sprite>(loadPath+clue.Value.id));
            }
        }
    }

    private void ShowClue()
    {
        int index = 0;
        foreach (var clue in _currentClueList.OrderBy(t=>t.Key))
        {
            GameObject clueIcon = Instantiate(_clueIconPrefabs, this.transform);
            clueIcon.name = clue.Key;
            clueIcon.GetComponent<Image>().sprite = _clueIconImageList[clueIcon.name];
            clueIcon.transform.position = new Vector3(-550 + 200 * index++, 250, 100);
        }
    }

    private void CheckClick(RaycastHit2D hit)
    {
        if (_currentClueList.ContainsKey(hit.transform.name))
        {
            ClearDetail();
            string loadPath = "Clue/Prefabs/";
            Instantiate(Resources.Load<GameObject>(loadPath+hit.transform.name+"_detail"),_detail.transform);
        }
    }

    private void ClearDetail()
    {
        for (int i = 0; i < _detail.transform.childCount; i++)
        {
            Destroy(_detail.transform.GetChild(i).gameObject);
        }
    }
}
