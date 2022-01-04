using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeMode
{
    FadeIn,
    FadeOut
}

public class FadeSystem : MonoBehaviour
{
    #region Singleton

    private static FadeSystem _instance;

    public static FadeSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<FadeSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("FadeSystem");
                _instance = gameObject.AddComponent<FadeSystem>();
            }
        }
        
        return _instance;
    }

    #endregion

    private GameObject _fadeObject;
    private Image _fadeObjectImage;

    private float _fadeSpeed = 0.01f;

    private Action _fadeAnimation = delegate {};
    private Action _callBack = delegate { };

    private void OnEnable()
    {
        _fadeObject = this.transform.GetChild(0).gameObject;
        _fadeObjectImage = _fadeObject.GetComponent<Image>();
        Color color = _fadeObjectImage.color;
        color.a = 0;
        _fadeObjectImage.color = color;
    }

    public void CallFadeSystem(FadeMode mode)
    {
        switch (mode)
        {
            case FadeMode.FadeIn:
                _fadeAnimation = FadeInAnimation;
                break;
            
            case FadeMode.FadeOut:
                _fadeAnimation = FadeOutAnimation;
                break;
        }
    }

    public void SetCallBack(Action func)
    {
        _callBack = func;
    }

    private void ResetCallBack()
    {
        _callBack = delegate {};
    }
    
    private void FadeInAnimation()
    {
        Color color = _fadeObjectImage.color;
        if (color.a<0.05)
        {
            color.a = 0;
            _fadeObjectImage.color = color;
            _fadeAnimation = delegate {};
            _callBack();
            ResetCallBack();
            return;
        }
        color.a -= _fadeSpeed;
        _fadeObjectImage.color = color;
    }

    private void FadeOutAnimation()
    {
        Color color  = _fadeObjectImage.color;
        if (color.a > 0.95)
        {
            color.a = 1;
            _fadeObjectImage.color = color;
            _fadeAnimation = delegate {};
            _callBack();
            ResetCallBack();
            return;
        }
        color.a += _fadeSpeed;
        _fadeObjectImage.color = color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CallFadeSystem(FadeMode.FadeIn);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            CallFadeSystem(FadeMode.FadeOut);
        }
        _fadeAnimation();
    }
}
