using UnityEngine;

namespace StoryBoardEditor
{
    public class GameSystem : MonoBehaviour
    {
        void Update()
        {
            WorldManipulator.GetInstance().Scroll();
            WorldManipulator.GetInstance().CameraMove();

            if (Input.GetKeyDown(KeyCode.A))
            {
                NodeManager.GetInstance().AddNode();
            }

        }
    }
}

