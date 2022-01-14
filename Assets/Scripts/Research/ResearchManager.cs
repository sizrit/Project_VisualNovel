using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager
{
    #region Singleton

    private static ResearchManager _instance;

    public static ResearchManager GetInstance()
    {
        if(_instance==null)
        {
            _instance = new ResearchManager();
        }
        return _instance;
    }

    #endregion

    public void SetResearch(string researchId)
    {
        ResearchObjectSetLoadManger.GetInstance().LoadObjectSet(researchId);
        ResearchEdgeController.GetInstance().SetEdgeControl(researchId);
    }

    
    
    // bg load
    
    //object set load
    
    
    
    
}
