using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryBoardSystem
{
    public enum EventId
    {
        Null,
        E01,
        E02
    }
    
    public class StoryBoardEventManager 
    {
        #region SingleTon

        private static StoryBoardEventManager _instance;

        public static StoryBoardEventManager GetInstance()
        {
            if (_instance == null)
            {
                _instance= new StoryBoardEventManager();
            }

            return _instance;
        }

        #endregion

        private string _currentStoryBoardId = "";
    
        private readonly Dictionary<string, Action> _eventList = new Dictionary<string, Action>();

        public void MakeEventList()
        {
            List<string> selectionIdList = StoryBoardSelectionEventDataLoadManager.GetInstance().GetSelectionEventId();
            foreach (var selectionId in selectionIdList)
            {
                _eventList.Add(selectionId,SelectionEvent);
            }
        
            IEnumerable<string> gainClueIdList = ClueManager.GetInstance().GetGainClueEventStoryBoardIdList();
            foreach (var gainClueId in gainClueIdList)
            {
                _eventList.Add(gainClueId,GainClueEvent);
            }
        }

        public bool IsStoryBoardEvent(string storyBoardIdValue)
        {
            return _eventList.ContainsKey(storyBoardIdValue);
        }

        public void StoryBoardEventOn(string storyBoardIdValue)
        {
            _currentStoryBoardId = storyBoardIdValue;
            _eventList[storyBoardIdValue]();
        }

        private void SelectionEvent()
        {
            StoryBoardSelectionEventManager.GetInstance().SetSelectionEvent(_currentStoryBoardId);
        }

        private void GainClueEvent()
        {
            StoryBoardGainClueEventManager.GetInstance().SetGainClueEvent(_currentStoryBoardId);
        }

        public static EventId ConvertToEventId(string stringValue)
        {
            List<EventId> clueList = Enum.GetValues(typeof(EventId)).Cast<EventId>().ToList();
            foreach (var clue in clueList)
            {
                if (stringValue == clue.ToString())
                {
                    return clue;
                }
            }

            return EventId.Null;
        }
    }
}
