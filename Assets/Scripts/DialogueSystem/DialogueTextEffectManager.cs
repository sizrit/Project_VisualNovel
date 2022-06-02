using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DialogueSystem
{
    public enum DialogueTextEffectId
    {
        Null,
        A,
    }

    public class DialogueTextEffectManager : MonoBehaviour
    {
        #region Singleton

        private static DialogueTextEffectManager _instance;

        public static DialogueTextEffectManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<DialogueTextEffectManager>();
                if (obj == null)
                {
                    Debug.LogError("Error! DialogueTextEffectManager is disable now");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
    
        readonly Dictionary<string,Action> _effectList =new Dictionary<string,Action>();

        private Action _effectDelegate = delegate { };

        [SerializeField] private GameObject currentTextGameObject;
        [SerializeField] private GameObject pastTextGameObject;
    
        private void Func0(){}

        private void MakeEffectList()
        {
        }
    
        public void SetDialogueTextEffect(string storyBoardIdValue)
        {
            if (_effectList.ContainsKey(storyBoardIdValue))
            {
                _effectDelegate+=_effectList[storyBoardIdValue];
            }
        }

        public void EndEffect()
        {
            _effectDelegate = delegate { };
            currentTextGameObject.transform.localPosition = new Vector3(0,0,0);
            pastTextGameObject.transform.localPosition = new Vector3(0,0,0);
        }

        private void Shake()
        {
            float randY = Random.Range(-10, 10);
            float randX = Random.Range(-3, 3);
            currentTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
            pastTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
        }

        public void Awake()
        {
            MakeEffectList();
        }

        public void Update()
        {
            _effectDelegate();
        }

        public static DialogueTextEffectId ConvertStringToDialogueTextEffectId(string stringValue)
        {
            List<DialogueTextEffectId> idList = Enum.GetValues(typeof(DialogueTextEffectId)).Cast<DialogueTextEffectId>()
                .ToList();

            foreach (var id in idList)
            {
                if (stringValue == id.ToString())
                {
                    return id;
                }
            }

            return DialogueTextEffectId.Null;
        }
    }
}