using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickSystem
{
    public enum ClickMode
    {
        StoryBoard,
        Menu,
        Research,
        Disable
    }

    public class ClickSystem
    {
        #region Singleton

        private static ClickSystem _instance;

        public static ClickSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance=new ClickSystem();
            }
            return _instance;
        }

        #endregion
    
        readonly Dictionary<ClickMode,I_ClickSystem> _clickSystemList = new Dictionary<ClickMode, I_ClickSystem>();
        private ClickMode _currentClickMode = ClickMode.StoryBoard;

        private bool _isClickEnable = false;
    
        public void Initialize()
        {
            _clickSystemList.Add(ClickMode.StoryBoard,StoryBoardClickSystem.GetInstance());
            _clickSystemList.Add(ClickMode.Menu,UI_GameMenuClickSystem.GetInstance());
            //_clickSystemList.Add(ClickMode.Research,ResearchClickSystem.GetInstance());
            //_clickSystemList.Add(ClickMode.Disable,);
            _isClickEnable = true;
        }

        public void DisableClick()
        {
            _isClickEnable = false;
        }

        public void EnableClick()
        {
            _isClickEnable = true;
        }

        public void SetClickMode(ClickMode mode)
        {
            _currentClickMode = mode;
        }

        private void Click()
        {
            _clickSystemList[_currentClickMode].Click();
        }
    
        public void Update()
        {
            if (_isClickEnable)
            {
                Click();
            }
        }
    }
}