using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class StoryBoardDataEditorGraphView : GraphView
    {
        public StoryBoardDataEditorGraphView()
        {
            AddGridBackGround();
            AddStyle();
            AddManipulators();
        }

        private StoryBoardNode CreateNode(Vector2 position)
        {
            StoryBoardNode node = new StoryBoardNode();
            node.Initialize(position);
            node.Draw();
            return node;
        }
        
        private void AddStyle()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("SizritStyle.uss");
            styleSheets.Add(styleSheet);
        }

        private void AddManipulators()
        {
            SetupZoom( ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(CreateNodeContextualMenu());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
        }

        private void AddGridBackGround()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0,gridBackground);
        }

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Node",actionEvent => AddElement(CreateNode(actionEvent.eventInfo.localMousePosition)))
                );
            return contextualMenuManipulator;
        }
    }
}
