using System.Collections.Generic;
using ClickSystem;
using UnityEngine;
using UnityEngine.UI;

namespace StoryBoardSystem
{
    public class StoryBoardSelectionEventManager : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardSelectionEventManager _instance;

        public static StoryBoardSelectionEventManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardSelectionEventManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    GameObject gameObject = new GameObject("");
                    _instance = gameObject.AddComponent<StoryBoardSelectionEventManager>();
                }
            }
            return _instance;
        }

        private void Awake()
        {
            var obj = FindObjectsOfType<StoryBoardSelectionEventManager>();
            if (obj.Length != 1)
            {
                Destroy(gameObject);
            }
        }

        #endregion

        private List<string> _selectionStoryBoardIdList = new List<string>();
        private List<string> _textList = new List<string>();

        [SerializeField] private GameObject selectionGameObject;

        private void RestObject()
        {
            _selectionStoryBoardIdList= new List<string>();
            _textList =new List<string>();
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        public void SetSelectionEvent(string storyBoardIdValue)
        {
            StoryBoardClickSystem.GetInstance().DisableStoryBoardCheckClick();

            SelectionInfo info = StoryBoardSelectionEventDataLoadManager.GetInstance().GetStoryBoardSelectionEventData(storyBoardIdValue);
            _selectionStoryBoardIdList = info.nextStoryIdList;
            _textList = info.textList;

            ShowSelectionEvent();
        }

        private void ShowSelectionEvent()
        {
            for (int i = 0; i < _selectionStoryBoardIdList.Count; i++)
            {
                GameObject obj =  Instantiate(selectionGameObject, this.transform);
                obj.transform.localPosition = new Vector3(0, 100 * _selectionStoryBoardIdList.Count - 200 * i, 0);
                obj.transform.GetChild(0).GetComponent<Text>().text = _textList[i];
            }
            StoryBoardClickSystem.GetInstance().SubscribeCheckClick(CheckClick);
        }

        private void CheckClick(RaycastHit2D hit)
        {
            for (int i = 0; i < _selectionStoryBoardIdList.Count; i++)
            {
                if (hit.transform == this.transform.GetChild(i))
                {
                    StoryBoardManager.GetInstance().SetNextStoryBoard(_selectionStoryBoardIdList[i]);
                    StoryBoardManager.GetInstance().SetStoryBoard();

                    RestObject();
                
                    StoryBoardClickSystem.GetInstance().UnsubscribeCheckClick(CheckClick);
                    StoryBoardClickSystem.GetInstance().EnableStoryBoardCheckClick();
                }
            }
        }
    }
}
