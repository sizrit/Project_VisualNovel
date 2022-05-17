using System.Diagnostics.CodeAnalysis;
using ClickSystem;
using UnityEngine;

namespace UI_GameMenu
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UI_GameMenu_Setting : MonoBehaviour
    {
        private void OnEnable()
        {
            UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
        }

        private void CheckClick(RaycastHit2D hit)
        {

        }
    }
}
