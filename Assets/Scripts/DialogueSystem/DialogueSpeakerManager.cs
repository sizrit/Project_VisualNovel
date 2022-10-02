using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueSpeakerManager : MonoBehaviour
    {
        #region Singleton

        private static DialogueSpeakerManager _instance;

        public static DialogueSpeakerManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<DialogueSpeakerManager>();
                if (obj == null)
                {
                    Debug.Log("Error! DialogueSpeakerManager is disable now");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion

        [SerializeField] private GameObject speaker;

        public void SetSpeaker(string stringValue)
        {
            speaker.GetComponent<Text>().text = stringValue;
        }
    }
}
