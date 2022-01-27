using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorGameSystem : MonoBehaviour
    {
        void Update()
        {
            StoryBoardManagerManipulator.GetInstance().Scroll();
            StoryBoardManagerManipulator.GetInstance().CameraMove();

            StoryBoardEditorClickSystem.GetInstance().CheckClick();
            
            StoryBoardEditorClickSystem.GetInstance().CheckDrag();

            if (Input.GetKeyDown(KeyCode.D))
            {
                StoryBoardEditorClickSystem.GetInstance().DeleteCheck();
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                StoryBoardEditorNodeManager.GetInstance().AddNode();
            }

        }
    }
}

