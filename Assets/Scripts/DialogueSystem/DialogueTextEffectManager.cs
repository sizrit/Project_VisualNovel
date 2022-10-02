using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DialogueSystem
{
    public enum DialogueTextEffectId
    {
        Null,
        Shake,
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
    
        private readonly Dictionary<DialogueTextEffectId,Action> effectList =new Dictionary<DialogueTextEffectId,Action>();

        private Action _effectDelegate = delegate { };

        [SerializeField] private GameObject currentTextGameObject;
        [SerializeField] private GameObject pastTextGameObject;

        public void Initialize() // 초기화
        {
            // GameSystem 에서 관리하는 Update Event Function안에서 동작할 함수 등록
            GameSystem.GetInstance().SubscribeUpdateFunction("DialogueTextEffectManager", _effectDelegate);
            
            effectList.Add(DialogueTextEffectId.Null, delegate { });
            effectList.Add(DialogueTextEffectId.Shake, Shake);
        }
    
        public void SetDialogueTextEffect(string effectIdIdValue) // Effect Id 를 받아서 해당 Effect 실행
        {
            DialogueTextEffectId id = ConvertStringToDialogueTextEffectId(effectIdIdValue);
            if (effectList.ContainsKey(id))
            {
                _effectDelegate += effectList[id];
            }
        }

        public void EndEffect() // Effect 종료시 호출
        {
            _effectDelegate = delegate { };
            currentTextGameObject.transform.localPosition = new Vector3(0,0,0);
            pastTextGameObject.transform.localPosition = new Vector3(0,0,0);
        }

        private void Shake() // Custom Effect 함수
        {
            float randY = Random.Range(-10, 10);
            float randX = Random.Range(-3, 3);
            currentTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
            pastTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
        }

        public static DialogueTextEffectId ConvertStringToDialogueTextEffectId(string stringValue) // string -> DialogueTextEffectId 로 전환
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