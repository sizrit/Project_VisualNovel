using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardSystem
{
    public enum StoryBoardMode
    {
        Dialogue,
        Selection,
        GainClue,
        GetItem,
        SwitchToResearch,
        ReturnToReserch,
    }

    [Serializable]
    public struct StoryBoard
    {
        public string storyBoardId;
        public StoryBoardMode mode;
        public BgId bgId;
        public string imageId;
        public string nextStoryBoardId;

        public StoryBoard(string storyBoardId,StoryBoardMode mode, BgId bgId, string imageId, string nextStoryBoardId)
        {
            this.storyBoardId = storyBoardId;
            this.mode = mode;
            this.bgId = bgId;
            this.imageId = imageId;
            this.nextStoryBoardId = nextStoryBoardId;
        }
    }
}