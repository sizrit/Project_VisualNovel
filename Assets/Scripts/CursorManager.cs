using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CursorName
{
    Idle,
    Eye,
    Return,
    Search,
    Hold,
    Go
}

public class CursorManager
{
    #region Singleton

    private static CursorManager _instance;

    public static CursorManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CursorManager();
        }
        return _instance;
    }
    
        #endregion
    
    Dictionary<CursorName, Texture2D> _texture2DList = new Dictionary<CursorName, Texture2D>();

    public void LoadCursor()
    {
        List<CursorName> cursorModes = Enum.GetValues(typeof(CursorName)).Cast<CursorName>().ToList();

        string path = "Cursor";
        Texture2D[] texture2Ds = Resources.LoadAll<Texture2D>(path);
        foreach (var texture2D in texture2Ds)
        {
            foreach (var mode in cursorModes)
            {
                if (mode.ToString() == texture2D.name)
                {
                    _texture2DList.Add(mode,texture2D);
                }
            }
        }
        
        ChangeCursorMode(CursorName.Idle);
    }
    
    public void ChangeCursorMode(CursorName mode)
    {
        Cursor.SetCursor(_texture2DList[mode],Vector2.zero, CursorMode.ForceSoftware);
    }

}
