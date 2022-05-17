using System;
using System.Collections;
using System.Collections.Generic;
using ClickSystem;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_GameMenu_Inventory : MonoBehaviour
{
    readonly ItemManager _itemManager = ItemManager.GetInstance();
    private IEnumerable<Item> _currentItemList;
    
    [SerializeField]
    private int lineCount = 4;

    
    [SerializeField] private Item currentDetail = Item.Null;
    
    private GameObject _detailParentGameObject;
    
    Dictionary<Item,GameObject> _detailPrefabList = new Dictionary<Item, GameObject>();
    
    private void ShowInventory()
    {
        GameObject parentGameObject = GameObject.Find("InventoryItemList");
        int index = 0;
        foreach (var item  in _currentItemList)
        {
            string loadPath = "UI_GameMenu/Inventory/Prefabs/" + item;
            GameObject itemPrefab = Instantiate(Resources.Load<GameObject>(loadPath),parentGameObject.transform);
            itemPrefab.name = item.ToString();
            SortInventory(itemPrefab,index++);
        }
    }

    private void SortInventory(GameObject prefabs, int index)
    {
        int line = (index + 1) % lineCount;
        int row = (index + 1 - line) / lineCount;
        prefabs.transform.position = new Vector3((400 - line*200)*-1, 300 -200*row,100);
    }
    
    private void OnEnable()
    {
        currentDetail = Item.Null;
        _currentItemList = _itemManager.GetCurrentItemList();
        ShowInventory();
        UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
        _detailParentGameObject = GameObject.Find("InventoryDetail");
        LoadDetail();
    }

    private void CheckClick(RaycastHit2D hit)
    {
        foreach (var item in _currentItemList)
        {
            if (item.ToString() == hit.transform.name)
            {
                Debug.Log("click" + hit.transform.name);
                ShowDetail(item);
            }
        }
    }

    private void ShowDetail(Item item)
    {
        if (currentDetail != item)
        {
            currentDetail = item;
            Instantiate(_detailPrefabList[item], _detailParentGameObject.transform);
        }
    }

    private void LoadDetail()
    {
        foreach (var item in _currentItemList)
        {
            string loadPath = "UI_GameMenu/Inventory/Prefabs/" + item + "_detail";
            _detailPrefabList.Add(item,Resources.Load<GameObject>(loadPath));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _itemManager.AddItem(Item.Item01);
            _itemManager.AddItem(Item.Item02);   
        }
    }
}
