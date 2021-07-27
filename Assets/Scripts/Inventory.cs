using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Clue> _currentClueList = new List<Clue>();
    private Dictionary<string,Sprite> _clueIconImageList = new Dictionary<string, Sprite>();

    private GameObject _clueIconPrefabs;

    private void OnEnable()
    {
        ClueManager.GetInstance().GetClue(0);
        ClueManager.GetInstance().GetClue(1);
        _currentClueList = ClueManager.GetInstance().GetCurrentClueList();
        LoadPrefabs();
        LoadImages();
        ShowClue();
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
            if (!_clueIconImageList.ContainsKey(clue.name))
            {
                _clueIconImageList.Add(clue.name,Resources.Load<Sprite>(loadPath+clue.name));
            }
        }
    }

    private void ShowClue()
    {
        
        for(int i=0; i< _currentClueList.Count; i++)
        {
            GameObject clueIcon = Instantiate(_clueIconPrefabs, this.transform);
            clueIcon.name = _currentClueList[i].name;
            clueIcon.GetComponent<Image>().sprite = _clueIconImageList[clueIcon.name];
            clueIcon.transform.position = new Vector3(-550 + 200 * i, 250, 100);
        }
    }
    
    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

            CheckClose(hitList);
        }
    }
    
    
    private void CheckClose(RaycastHit2D[] hitListValue)
    {
        bool isCheck = false;
        foreach (var hit in hitListValue)
        {
            if (hit.transform == this.gameObject.transform)
            {
                isCheck = true;
            }
        }

        if (!isCheck)
        {
            ClearAllChild();
            GameSystem.GetInstance().PauseOff();
            this.gameObject.SetActive(false);
        }
    }

    private void ClearAllChild()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        Click();
    }
}
