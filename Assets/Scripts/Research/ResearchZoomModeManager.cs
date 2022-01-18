using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchZoomModeManager : MonoBehaviour
{
    Dictionary<string,GameObject> _zoomObjectSetList = new Dictionary<string, GameObject>();

    private string _zoomObjectSetId = "";

    public void LoadZoomObjectSet()
    {
        string path = "ResearchZoomObjectSet";
        GameObject[] gameObjects = Resources.LoadAll<GameObject>(path);
        foreach (var objectSet in gameObjects)
        {
            _zoomObjectSetList.Add(objectSet.name,objectSet);
        }
    }
    
    public void StartZoomMode(string id)
    {
        _zoomObjectSetId = id;
        ClickSystem.GetInstance().DisableClick();
        FadeSystem.GetInstance().SetCallBack(StartZoomModeCallBack01);
        FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeOut);
    }

    private void StartZoomModeCallBack01()
    {
        FadeSystem.GetInstance().SetCallBack(StartZoomModeCallBack02);
        ResearchObjectSetLoadManger.GetInstance().DisableObjectSet();
        Instantiate(_zoomObjectSetList[_zoomObjectSetId], this.transform);
        FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeIn);
    }

    private void StartZoomModeCallBack02()
    {
        ClickSystem.GetInstance().EnableClick();
    }
    
    public void EndZoomMode()
    {
        ClickSystem.GetInstance().DisableClick();
        FadeSystem.GetInstance().SetCallBack(EndZoomModeCallBack01);
        FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeOut);
    }

    private void EndZoomModeCallBack01()
    {
        FadeSystem.GetInstance().SetCallBack(EndZoomModeCallBack02);
        ResearchObjectSetLoadManger.GetInstance().EnableObjectSet();
        ResetZoomMode();
        FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeIn);
    }

    private void EndZoomModeCallBack02()
    {
        ClickSystem.GetInstance().EnableClick();
    }

    private void ResetZoomMode()
    {
        _zoomObjectSetId = "";
        Destroy(this.transform.GetChild(0).gameObject);
    }
        
}
