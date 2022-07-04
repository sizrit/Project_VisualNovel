using DialogueSystem;

namespace StoryBoardSystem
{
    public class StoryBoardManager
    {
        #region Singleton

        private static StoryBoardManager _instance;

        public static StoryBoardManager GetInstance()
        {
            if (_instance == null)
            {
                _instance=new StoryBoardManager();
            }
            return _instance;
        }

        #endregion
    
        private StoryBoard _currentStoryBoard;

        public void TestRun()
        {
            _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard("S0000");
        }

        private void SetNextStoryBoard()
        {
            string nextStoryBoardId = _currentStoryBoard.nextStoryBoardId;
            _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard(nextStoryBoardId);
        }
    
        public void SetNextStoryBoard(string storyBoardIdValue)
        {
            _currentStoryBoard =StoryBoardDataLoadManager.GetInstance().GetStoryBoard(storyBoardIdValue);
        }

        public void SetStoryBoard()
        {
            if (DialogueManager.GetInstance().CheckIsAnimationEnd())
            {
                StoryBoardBgLoadManager.GetInstance().SetBg(_currentStoryBoard.bgId);
                StoryBoardImageLoadManager.GetInstance().SetImage(_currentStoryBoard.imageId);
            
                switch (_currentStoryBoard.mode)
                {
                    case StoryBoardMode.Dialogue:
                        DialogueManager.GetInstance().SetDialogue(_currentStoryBoard.storyBoardId);
                        SetNextStoryBoard();
                        break;
                
                    case StoryBoardMode.Selection:
                        StoryBoardSelectionEventManager.GetInstance().SetSelectionEvent(_currentStoryBoard.storyBoardId);
                        break;
                    
                    case StoryBoardMode.GetClue:
                        StoryBoardGainClueEventManager.GetInstance().SetGainClueEvent(_currentStoryBoard.storyBoardId);
                        break;
                    
                    case StoryBoardMode.GetItem:
                    
                        break;
                
                    case  StoryBoardMode.SwitchToResearch:
                        StoryBoardSwitchManager.GetInstance().Switch(_currentStoryBoard.storyBoardId);
                        break;
                
                    case StoryBoardMode.ReturnToResearch:
                    
                        break;
                }
            }
            else
            {
                DialogueManager.GetInstance().EndAnimationForced();
            }
        }
    }
}
