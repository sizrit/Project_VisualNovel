using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor
{
    public class StoryBoardDataEditorGraphView : GraphView
    {
        public StoryBoardDataEditorGraphView()
        {
            //AddGridBackGround();
        }

        private void AddGridBackGround()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
        
            Insert(0,gridBackground);
        }
    }
}
