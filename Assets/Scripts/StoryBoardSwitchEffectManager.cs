using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardSwitchEffectManager : MonoBehaviour
{
    #region Singleton
    
    private static StoryBoardSwitchEffectManager _instance;

    public static StoryBoardSwitchEffectManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardSwitchEffectManager>();
            
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameModeManager");
                _instance = gameObject.AddComponent<StoryBoardSwitchEffectManager>();
            }
        }
        return _instance;
    }

    #endregion

    [SerializeField] private GameObject gameObject01;

    private float _speed = 0.01f;
    
    private Action _callBack = delegate { };
    private Action _effect = delegate { };

    public void SwitchOnEffect(Action func)
    {
        ResetCallBack();
        //FadeSystem.GetInstance().SetCallBack(func);
        //FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeIn);
         _effect = SwitchOnEffectAnimation;
         _callBack = func;
    }

    public void SwitchOffEffect(Action func)
    {
        ResetCallBack();
        // FadeSystem.GetInstance().SetCallBack(func);
        // FadeSystem.GetInstance().CallFadeSystem(FadeMode.FadeOut);
        
         _effect = SwitchOffEffectAnimation;
         _callBack = func;
    }

    // effect
    private void SwitchOnEffectAnimation()
    {
        Color color = gameObject01.GetComponent<Image>().color;
        if (color.a > 0.95)
        {
            color.a = 1;
            gameObject01.GetComponent<Image>().color = color;
            _effect = delegate { };
            _callBack();
            return;
        }

        color.a += _speed;
        gameObject01.GetComponent<Image>().color = color;
        //gameObject02.GetComponent<Image>().color = color;
        //gameObject03.GetComponent<Image>().color = color;
    }

    // effect
    private void SwitchOffEffectAnimation()
    {
        Color color = gameObject01.GetComponent<Image>().color;
        if (color.a < 0.05)
        {
            color.a = 0;
            gameObject01.GetComponent<Image>().color = color;
            _effect = delegate { };
            _callBack();
            return;
        }

        color.a -= _speed;
        gameObject01.GetComponent<Image>().color = color;
    }

    private void ResetCallBack()
    {
        _callBack = delegate { };
    }
    
    public void Update()
    {
        _effect();
    }
}
