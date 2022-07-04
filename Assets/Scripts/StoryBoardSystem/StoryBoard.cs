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
        GetClue,
        GetItem,
        SwitchToResearch,
        ReturnToResearch,
        Event,
        Null
    }

    [Serializable]
    public class StoryBoard
    {
        public string storyBoardId;
        public StoryBoardMode mode;
        public BgId bgId;
        public ImageId imageId;
        public string selectionId;
        public Clue clueId;
        public Item itemId;
        public EventId eventId;
        public StoryBoard nextStoryBoard;

        public StoryBoard()
        {
        }

        public StoryBoard(string storyBoardId,StoryBoardMode mode, BgId bgId, ImageId imageId)
        {
            this.storyBoardId = storyBoardId;
            this.mode = mode;
            this.bgId = bgId;
            this.imageId = imageId;
        }
    }
}