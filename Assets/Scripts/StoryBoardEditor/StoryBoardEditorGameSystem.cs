using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorGameSystem : MonoBehaviour
    {
        void Update()
        {
            StoryBoardManagerManipulator.GetInstance().Scroll();
            StoryBoardManagerManipulator.GetInstance().CameraMove();
        }
    }
}

