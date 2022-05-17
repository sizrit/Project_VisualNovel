using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ClickSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI_GameMenu
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UI_GameMenu_ClueInventory : MonoBehaviour
    {
        private IEnumerable<Clue> _currentClueList;
        private readonly Dictionary<Clue,Sprite> _clueIconImageList = new Dictionary<Clue, Sprite>();

        private GameObject _clueIconPrefabs;

        private GameObject _detail;

        [SerializeField] private string currentClueDetail = "";

        private void OnEnable()
        {
            _detail = GameObject.Find("Detail");

            _currentClueList = ClueManager.GetInstance().GetCurrentClueList();
            LoadPrefabs();
            LoadImages();
            ShowClue();
        
            UI_GameMenuClickSystem.GetInstance().SubScribeCheckClickFunc(CheckClick);
        }

        private void LoadPrefabs()
        {
            string loadPath = "UI_GameMenu/ClueInventory/Prefabs/ClueIcon";
            _clueIconPrefabs = UnityEngine.Resources.Load<GameObject>(loadPath);
        }

        private void LoadImages()
        {
            string loadPath = "UI_GameMenu/ClueInventory/Images/";
            foreach (var clue in _currentClueList)
            {
                if (!_clueIconImageList.ContainsKey(clue))
                {
                    _clueIconImageList.Add(clue,Resources.Load<Sprite>(loadPath+clue+"_Icon"));
                }
            }
        }

        private void ShowClue()
        {
            foreach (var clue in _currentClueList.OrderBy(t=>t))
            {
                GameObject clueIcon = Instantiate(_clueIconPrefabs, this.transform);
                clueIcon.name = clue.ToString();
                clueIcon.GetComponent<Image>().sprite = _clueIconImageList[clue];
            }
        }

        private void CheckClick(RaycastHit2D hit)
        {
            foreach (var clue in _currentClueList)
            {
                if (clue.ToString() == hit.transform.name)
                {
                    ShowDetail(hit);
                }
            }
        }

        private void ShowDetail(RaycastHit2D hit)
        {
            string clueName = hit.transform.name;
            if (currentClueDetail != clueName)
            {
                currentClueDetail = clueName;
                ClearDetail();
                string loadPath = "UI_GameMenu/ClueInventory/Prefabs/";
                Instantiate(Resources.Load<GameObject>(loadPath+hit.transform.name+"_detail"),_detail.transform);
            }
        }

        private void ClearDetail()
        {
            for (int i = 0; i < _detail.transform.childCount; i++)
            {
                Destroy(_detail.transform.GetChild(i).gameObject);
            }
        }
    }
}
