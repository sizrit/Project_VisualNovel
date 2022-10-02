using System;
using UI_GameMenu;

namespace DialogueSystem
{
    [Serializable]
    public struct Dialogue
    {
        public string storyBoardId;
        public string dialogueText;
        public string effect;
        public string color;
    }

    public enum Chapter
    {
        Chapter01,
    }

    public class DialogueManager
    {
        #region Singleton

        private static DialogueManager _instance;

        public static DialogueManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DialogueManager();
            }

            return _instance;
        }

        #endregion

        public bool CheckIsAnimationEnd() // Dialogue 출력 Animation 의 종료 여부를 반환
        {
            return DialogueTextAnimationManager.GetInstance().GetIsAnimationEnd();
        }

        public void EndAnimationForced() // 강제로 Dialogue 출력 Animation 을 종료시킴
        {
            DialogueTextAnimationManager.GetInstance().EndAnimationForced();
            DialogueTextEffectManager.GetInstance().EndEffect();
        }

        public void AnimationEnd() // Dialogue 출력 Animation 의 종료시점에 호출
        {
            DialogueTextEffectManager.GetInstance().EndEffect();
        }
    
        public void SetDialogue(string storyBoardIdValue) // Dialogue 출력
        {
            // 현재 storyBoardId 를 기준으로 Dialogue 를 불러옴
            Dialogue currentDialogue = DialogueDataLoadManager.GetInstance().GetDialogue(storyBoardIdValue);
            
            // Dialogue 를 Animation 으로 출력
            DialogueTextAnimationManager.GetInstance().PlayDialogueTextAnimation(currentDialogue.dialogueText);
            
            // Dialogue 에 적용된 Effect 적용
            DialogueTextEffectManager.GetInstance().SetDialogueTextEffect(currentDialogue.effect);
            
            // Dialogue 에 적용된 전체적인 Color 적용
            DialogueTextColorManager.GetInstance().SetDialogueTextColor(currentDialogue.color);
        
            // 지금까지 있었던 Dialogue 들을 UI 에서 Log 로 살펴볼수있도록 저장
            UI_GameMenu_DialogueLogManager.GetInstance().AddDialogueLog(currentDialogue);
        }
    }
}