using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorLine
    {
        public StoryBoardNode node01;
        public StoryBoardNode node02;
    }

    public class StoryBoardEditorTempLine
    {
        public StoryBoardNode node;
        public LineRenderer lineRenderer;
    }


    public class StoryBoardEditorLineManager : MonoBehaviour
    {
        [SerializeField] private GameObject line;
        
        
        private void MakeLine()
        {
        
        }
        
        public void DrawTempLine(StoryBoardNode node)
        {
            GameObject lineObject = 
        }

        public void MovingPoint()
        {
            
        }
        
        
    }
}