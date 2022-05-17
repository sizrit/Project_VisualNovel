using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueTextColorManager : MonoBehaviour
    {
        #region Singleton

        private static DialogueTextColorManager _instance;

        public static DialogueTextColorManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<DialogueTextColorManager>();
                if (obj == null)
                {
                    Debug.LogError("Error! DialogueTextColorManager is disable now");
                    return null;
                }

                _instance = obj;

            }

            return _instance;
        }

        #endregion
    
        [SerializeField] private GameObject currentDialogueText;
        [SerializeField] private GameObject pastDialogueText;

        public void SetDialogueTextColor(string colorValue)
        {
            Color color = Color.white;
            switch (colorValue)
            {
                case "White":
                    color=Color.white;
                    break;
            
                case "Red":
                    color =Color.red;
                    break;
            
                case "Black":
                    color = Color.black;
                    break;
            }

            currentDialogueText.GetComponent<Text>().color = color;
            pastDialogueText.GetComponent<Text>().color = color;
        }
    }
}
