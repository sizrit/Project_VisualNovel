using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public class JsonTextDataList
    {
        public List<Dialogue> dialogueList;
    }

    public class DialogueDataLoadManager
    {
        #region Singleton

        private static DialogueDataLoadManager _instance;

        public static DialogueDataLoadManager GetInstance()
        {
            if (_instance == null)
            {
                _instance=new DialogueDataLoadManager();
            }
            return _instance;
        }

        #endregion

        private readonly Dictionary<string, Dialogue> _dialogueList = new Dictionary<string, Dialogue>();

        public Dialogue GetDialogue(string storyBoardIdValue)
        {
            return _dialogueList[storyBoardIdValue];
        }

        public void LoadJsonData()
        {
            var chapterList = Enum.GetValues(typeof(Chapter)).Cast<Chapter>().ToList();
            LanguageType type = LanguageManager.GetInstance().GetLanguageType();

            for (int i = 1; i < chapterList.Count + 1; i++)
            {
                string filePath = "JsonData/Chapter0"+i.ToString()+"DialogueData";
                switch (type)
                {
                    case LanguageType.English:
                        filePath += "_En";
                        break;
                
                    case LanguageType.Korean:
                        filePath += "_Ko";
                        break;
                }
            
                JsonTextDataList jsonData = LoadJsonFiles<JsonTextDataList>(filePath);
                MakeDictionary(jsonData.dialogueList);
            }
        }

        private void MakeDictionary(List<Dialogue> dialogueListValue)
        {
            foreach (var dialogue in dialogueListValue)
            {
                _dialogueList.Add(dialogue.storyBoardId,dialogue);
            }
        }
    
        private T LoadJsonFiles<T>(string loadPath)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
            return JsonUtility.FromJson<T>(jsonData.ToString());
        }
    }
}