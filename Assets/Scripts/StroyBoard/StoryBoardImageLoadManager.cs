using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoryBoardImageLoadManager : MonoBehaviour
{
    #region Singleton

    private static StoryBoardImageLoadManager _instance;

    public static StoryBoardImageLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardImageLoadManager>();
            if (obj == null)
            {
                Debug.LogError("Error! StoryBoardImageLoadManager is disable now");
                return null;
            }
            _instance = obj;
        }

        return _instance;
    }

    #endregion

    private readonly Dictionary<string, GameObject> _imagePrefabsList = new Dictionary<string, GameObject>();

    public void LoadAllPrefabs()
    {
        GameObject[] imagePrefabs = Resources.LoadAll<GameObject>("Images/Prefabs");
        foreach (var imagePrefab in imagePrefabs)
        {
            _imagePrefabsList.Add(imagePrefab.name,imagePrefab);
        }
    }

    public void SetImage(string imageIdValue)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        Instantiate(_imagePrefabsList[imageIdValue], this.transform);
    }
}
