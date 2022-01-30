using System;
using UnityEngine;

namespace StoryBoardEditor
{
    public enum ClickMode
    {
        Input,
        Output,
        Normal,
    }
    
    public class StoryBoardEditorClickSystem
    {
        #region Singleton

        private static StoryBoardEditorClickSystem _instance;

        public static StoryBoardEditorClickSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StoryBoardEditorClickSystem();
            }

            return _instance;
        }

        #endregion

        private GameObject _currentSelectedNode = null;
        private ClickMode mode;

        public void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                foreach (var hit in hits)
                {
                    if (hit.transform.CompareTag("StoryBoardNodeInput"))
                    {
                        mode = ClickMode.Input;
                    }
                    
                    if (hit.transform.CompareTag("StoryBoardNodeOutput"))
                    {
                        mode = ClickMode.Output;
                    }
                    
                    if (hit.transform.CompareTag("StoryBoardNode"))
                    {
                        mode = ClickMode.Normal;
                        _currentSelectedNode = hit.transform.gameObject;
                        return;
                    }
                }
                _currentSelectedNode = null;
            }
        }

        Vector3 _prevPosition =Vector3.zero;
        
        public void CheckDrag()
        {
            switch (mode)
            {
                case ClickMode.Input:
                    break;
                case ClickMode.Output:
                    break;
                case ClickMode.Normal:
                    CheckClick();
                    if (Input.GetMouseButtonDown(0))
                    {
                        _prevPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        _prevPosition.z = 0;
                    }
                    
                    if (Input.GetMouseButton(0) && _currentSelectedNode != null)
                    {
                        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        currentPosition.z = 0;
                        if (_prevPosition != currentPosition)
                        {
                            Vector3 delta = currentPosition - _prevPosition;
                            _currentSelectedNode.transform.position+= delta;
                            _prevPosition = currentPosition;
                        }
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        _currentSelectedNode.transform.position = StoryBoardEditorGridSystem.GetInstance()
                            .SetPositionToGrid(_currentSelectedNode.transform.position);
                    }
                    break;
            }
        }

        public void DeleteCheck()
        {
            if (_currentSelectedNode != null)
            {
                StoryBoardEditorNodeManager.GetInstance().DeleteNode(_currentSelectedNode);
                _currentSelectedNode = null;
            }
        }
    }
}

