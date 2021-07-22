using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueTextEffectManager : MonoBehaviour
{
    readonly Dictionary<string,EffectDelegate> _effectList =new Dictionary<string,EffectDelegate>();
    delegate void EffectDelegate();

    private EffectDelegate _effectDelegate;

    private GameObject _currentTextGameObject;
    private GameObject _pastTextGameObject;
    
    private void Func0(){}

    private void MakeEffectList()
    {
        EffectDelegate shake = Shake;
        _effectList.Add("Main0001",shake);
        _effectList.Add("Main0002",shake);
    }
    
    public void SetDialogueTextEffect(string dialogueIdValue)
    {
        if (_effectList.ContainsKey(dialogueIdValue))
        {
            _effectDelegate+=_effectList[dialogueIdValue];
        }
    }

    public void EndEffect()
    {
        _effectDelegate = new EffectDelegate(Func0);
        _currentTextGameObject.transform.localPosition = new Vector3(0,0,0);
        _pastTextGameObject.transform.localPosition = new Vector3(0,0,0);
    }

    private void Shake()
    {
        float randY = Random.Range(-10, 10);
        float randX = Random.Range(-3, 3);
        _currentTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
        _pastTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
    }

    private void OnEnable()
    {
        MakeEffectList();
        _effectDelegate=new EffectDelegate(Func0);
        _currentTextGameObject = this.transform.GetChild(1).gameObject;
        _pastTextGameObject = this.transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        _effectDelegate();
    }
}
