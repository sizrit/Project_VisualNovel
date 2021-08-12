using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameMenu_ClueInventory : MonoBehaviour
{
    private List<string> _currentClueList = new List<string>();
    readonly private Dictionary<string,Sprite> _clueIconImageList = new Dictionary<string, Sprite>();

    private GameObject _clueIconPrefabs;

    private GameObject _detail;

    private void OnEnable()
    {
        _detail = GameObject.Find("Detail");

        _currentClueList = ClueManager.GetInstance().GetCurrentClueList();
        LoadPrefabs();
        LoadImages();
        ShowClue();
        
        UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
    }

    private void LoadPrefabs()
    {
        string loadPath = "UI_GameMenu/ClueInventory/Prefabs/ClueIcon";
        _clueIconPrefabs = Resources.Load<GameObject>(loadPath);
    }

    private void LoadImages()
    {
        string loadPath = "UI_GameMenu/ClueInventory/Images/";
        foreach (var clueId in _currentClueList)
        {
            if (!_clueIconImageList.ContainsKey(clueId))
            {
                _clueIconImageList.Add(clueId,Resources.Load<Sprite>(loadPath+clueId+"_Icon"));
            }
        }
    }

    private void ShowClue()
    {
        int index = 0;
        foreach (var clueId in _currentClueList.OrderBy(t=>t))
        {
            GameObject clueIcon = Instantiate(_clueIconPrefabs, this.transform);
            clueIcon.name = clueId;
            clueIcon.GetComponent<Image>().sprite = _clueIconImageList[clueIcon.name];
            clueIcon.transform.position = new Vector3(-550 + 200 * index++, 250, 100);
        }
    }

    private void CheckClick(RaycastHit2D hit)
    {
        if (_currentClueList.Contains(hit.transform.name))
        {
            ClearDetail();
            string loadPath = "UI_GameMenu/ClueInventory/Prefabs/";
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
