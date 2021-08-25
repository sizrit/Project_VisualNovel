using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Item
{
    Item01,
    Item02,
    Item03,
    Null
}

public class ItemManager
{
    #region Singleton

    private static ItemManager _instance;

    public static ItemManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ItemManager();
        }
        return _instance;
    }
    
    #endregion
    
    private List<Item> _allItemList= new List<Item>();
    private readonly List<Item> _currentItemList = new List<Item>();

    private void Init()
    {
        _allItemList = Enum.GetValues(typeof(Item)).Cast<Item>().ToList();
    }

    public void AddItem(Item itemValue)
    {
        if (!_currentItemList.Contains(itemValue))
        {
            _currentItemList.Add(itemValue);
        }
    }

    public void RemoveItem(Item itemValue)
    {
        if (_currentItemList.Contains(itemValue))
        {
            _currentItemList.Remove(itemValue);
        }
    }

    public void ClearInventory()
    {
        _currentItemList.Clear();
    }

    public IEnumerable<Item> GetCurrentItemList()
    {
        return _currentItemList;
    }
}
