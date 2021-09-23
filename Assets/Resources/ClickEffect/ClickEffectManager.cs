using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectManager : MonoBehaviour
{
    #region Singleton

    private static ClickEffectManager _instance;

    public static ClickEffectManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ClickEffectManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                _instance = new GameObject("ClickEffectManager").AddComponent<ClickEffectManager>();
            }
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<ClickEffectManager>();
        if (obj.Length != 1)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    private GameObject _clickEffectPrefab;

    private void OnEnable()
    {
        string loadPath = "ClickEffect/ClickEffectPrefab";
        _clickEffectPrefab = Resources.Load<GameObject>(loadPath);
    }

    public void MakeClickEffect()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 100;
        Instantiate(_clickEffectPrefab,position,new Quaternion(0,0,0,0), this.transform);
    }
}
