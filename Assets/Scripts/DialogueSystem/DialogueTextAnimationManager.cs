using System;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public enum StyleTag
    {
        Color,
        Bold,
        Italic,
        Size
    }

    public class DialogueTextAnimationManager : MonoBehaviour
    {
        #region Singleton

        private static DialogueTextAnimationManager _instance;

        public static DialogueTextAnimationManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<DialogueTextAnimationManager>();
                if (obj == null)
                {
                    Debug.LogError("Error! DialogueTextAnimationManager is disable now");
                    return null;
                }

                _instance = obj;

            }

            return _instance;
        }


        #endregion

        [SerializeField] private GameObject currentDialogueText;
        [SerializeField] private GameObject pastDialogueText;

        private Action _dialogueTextManagerAction = delegate { };

        private string _dialogueTextData;
        private char[] _dialogueTextDataChar;
        private string _currentString = "";
        private string _pastString = "";
        private int _index = 0;

        private StyleTag style = StyleTag.Color;

        [SerializeField] private float fadeSpeed = 0.08f;

        private bool _isAnimationEnd = true;

        public void ChangeFadeSpeed(float speedValue)
        {
            fadeSpeed = speedValue;
        }

        public bool GetIsAnimationEnd()
        {
            return _isAnimationEnd;
        }

        public void Initialize()
        {
            GameSystem.GetInstance().SubscribeUpdateFunction("dialogueTextManager",_dialogueTextManagerAction);
        }

        public void EndAnimationForced()
        {
            _dialogueTextManagerAction = delegate { };
            currentDialogueText.GetComponent<Text>().text = _dialogueTextData;
            pastDialogueText.GetComponent<Text>().text = _dialogueTextData;
            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a = 1;
            currentDialogueText.GetComponent<Text>().color = color;
            pastDialogueText.GetComponent<Text>().color = color;
            _isAnimationEnd = true;
        }

        public void ResetDialogueTextAnimationManager()
        {
            currentDialogueText.GetComponent<Text>().text = "";
            pastDialogueText.GetComponent<Text>().text = "";
            _dialogueTextDataChar = "".ToCharArray();
            _currentString = "";
            _pastString = "";
            _index = 0;
        }

        public void PlayDialogueTextAnimation(string dialogueTextDataValue)
        {
            _isAnimationEnd = false;
            _dialogueTextData = dialogueTextDataValue;
            _dialogueTextDataChar = dialogueTextDataValue.ToCharArray();

            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a = 0;
            currentDialogueText.GetComponent<Text>().color = color;
            _dialogueTextManagerAction = delegate { };
            _dialogueTextManagerAction += DialogueTextAnimation_Add;
        }

        private void DialogueTextAnimation_FadeIn()
        {
            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a += fadeSpeed;
            if (color.a > 0.99)
            {
                color.a = 1;
                _dialogueTextManagerAction = delegate { };
                _dialogueTextManagerAction += DialogueTextAnimation_Add;
            }

            currentDialogueText.GetComponent<Text>().color = color;
        }

        private void DialogueRichTextAnimation_FadeIn()
        {
            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a += fadeSpeed;
            if (color.a > 0.99)
            {
                color.a = 1;
                _dialogueTextManagerAction = delegate { };
                _dialogueTextManagerAction += RichTextOn;
            }

            currentDialogueText.GetComponent<Text>().color = color;
        }


        private void EndTagOn()
        {
            _pastString += _dialogueTextDataChar[_index - 1];
            _currentString += _dialogueTextDataChar[_index];
            _index++;
            if (_dialogueTextDataChar[_index - 1] == '>')
            {
                _dialogueTextManagerAction = delegate { };
                _dialogueTextManagerAction += DialogueTextAnimation_Add;
            }
        }

        private void RichTextOn()
        {
            if (_dialogueTextDataChar[_index] == '<')
            {
                _dialogueTextManagerAction = delegate { };
                _dialogueTextManagerAction += EndTagOn;
                return;
            }

            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a = 0;
            currentDialogueText.GetComponent<Text>().color = color;

            _pastString += _dialogueTextDataChar[_index - 1];
            _currentString += _dialogueTextDataChar[_index];
            _index++;

            string endStyleTag = "";
            switch (style)
            {
                case StyleTag.Color:
                    endStyleTag = "</color>";
                    break;

                case StyleTag.Bold:
                    endStyleTag = "</b>";
                    break;

                case StyleTag.Italic:
                    endStyleTag = "</i>";
                    break;

                case StyleTag.Size:
                    endStyleTag = "</size>";
                    break;
            }

            currentDialogueText.GetComponent<Text>().text = _currentString + endStyleTag;
            pastDialogueText.GetComponent<Text>().text = _pastString + endStyleTag;

            _dialogueTextManagerAction = delegate { };
            _dialogueTextManagerAction += DialogueRichTextAnimation_FadeIn;
        }

        private void StartTagOn()
        {
            if (_index != 0)
            {
                _pastString += _dialogueTextDataChar[_index - 1];
            }

            _currentString += _dialogueTextDataChar[_index];
            _index++;
            if (_dialogueTextDataChar[_index - 1] == '>')
            {
                _dialogueTextManagerAction = delegate { };
                _dialogueTextManagerAction += RichTextOn;
            }
        }

        private void DialogueTextAnimation_Add()
        {
            if (_index == _dialogueTextDataChar.Length)
            {
                _dialogueTextManagerAction = delegate { };
                _isAnimationEnd = true;
                
                // Dialogue Animation 의 종료를 알리기위해 호출, Dialogue 에 적용된 Effect 또한 종료시킨다
                DialogueManager.GetInstance().AnimationEnd();   
                
                return;
            }

            if (_dialogueTextDataChar.Length != 0)
            {
                if (_dialogueTextDataChar[_index] == '<')
                {
                    switch (_dialogueTextDataChar[_index + 1])
                    {
                        case 'c':
                            style = StyleTag.Color;
                            break;

                        case 'b':
                            style = StyleTag.Bold;
                            break;

                        case 'i':
                            style = StyleTag.Italic;
                            break;

                        case 's':
                            style = StyleTag.Size;
                            break;
                    }

                    _dialogueTextManagerAction = delegate { };
                    _dialogueTextManagerAction += StartTagOn;
                    return;
                }
            }

            if (_index > 0)
            {
                _pastString += _dialogueTextDataChar[_index - 1];
                pastDialogueText.GetComponent<Text>().text = _pastString;
            }

            Color color = currentDialogueText.GetComponent<Text>().color;
            color.a = 0;
            currentDialogueText.GetComponent<Text>().color = color;

            if (_index < _dialogueTextDataChar.Length + 1)
            {
                _currentString += _dialogueTextDataChar[_index];
                currentDialogueText.GetComponent<Text>().text = _currentString;
            }

            _dialogueTextManagerAction = delegate { };
            _dialogueTextManagerAction += DialogueTextAnimation_FadeIn;

            _index++;
        }
    }
}
