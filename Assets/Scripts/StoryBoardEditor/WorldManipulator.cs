using UnityEngine;

namespace StoryBoardEditor
{
    public class WorldManipulator
    {
        #region Singleton

        private static WorldManipulator _instance;

        public static WorldManipulator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WorldManipulator();
            }

            return _instance;
        }
        
        #endregion
            
        private float _minSize = 4;
        private float _maxSize = 12;
        private float _speed = 0.4f;
        private Vector3 _prevPosition = Vector3.zero;

        public void Scroll()
        {
            if (Input.mouseScrollDelta.y > 0 && Camera.main.orthographicSize > _minSize)
            {
                if (Camera.main != null) Camera.main.orthographicSize -= _speed;
                GridSystem.GetInstance().AdjustGridLine();
            }

            if (Input.mouseScrollDelta.y < 0 && Camera.main.orthographicSize < _maxSize)
            {
                if (Camera.main != null) Camera.main.orthographicSize += _speed;
                GridSystem.GetInstance().AdjustGridLine();
            }
        }

        public void CameraMove()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _prevPosition = Input.mousePosition;
            }
            
            if (Input.GetMouseButton(1))
            {
                float speed = Camera.main.orthographicSize * 0.00185f;
                Vector3 currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
                if (currentPosition != _prevPosition)
                {
                    Camera.main.transform.position -= (currentPosition - _prevPosition) * speed;
                    _prevPosition = currentPosition;
                }
            }
        }
    }
}
