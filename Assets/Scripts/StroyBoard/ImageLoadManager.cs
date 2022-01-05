using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImageLoadManager : MonoBehaviour
{
    #region Singleton

    private static ImageLoadManager _instance;

    public static ImageLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ImageLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("ImageLoadManager");
                _instance = gameObject.AddComponent<ImageLoadManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<ImageLoadManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private readonly Dictionary<string, GameObject> _imagePrefabsList = new Dictionary<string, GameObject>();

    private void LoadAllPrefabs()
    {
        GameObject[] imagePrefabs = Resources.LoadAll<GameObject>("Images/Prefabs");
        foreach (var imagePrefab in imagePrefabs)
        {
            _imagePrefabsList.Add(imagePrefab.name,imagePrefab);
        }
    }

    private void OnEnable()
    {
        LoadAllPrefabs();
    }

    public void SetImage(string imageIdValue)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        if (imageIdValue != "")
        {
            Instantiate(_imagePrefabsList[imageIdValue], this.transform);
        }
    }
}
