using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForwardButton : MonoBehaviour
{
    private StoryBoardClickSystem _storyBoardClickSystem;
    private DialogueTextAnimationManager _dialogueTextAnimationManager;
    [SerializeField]
    private bool isOn = false;
    List<Sprite> _imageList = new List<Sprite>();
    
    private void OnEnable()
    {
        string loadPath = "Images/UI_Button/";
        _imageList.Add(Resources.Load<Sprite>(loadPath+"FFoff"));
        _imageList.Add(Resources.Load<Sprite>(loadPath+"FFon"));
        this.gameObject.GetComponent<Image>().sprite = _imageList[0];

        _storyBoardClickSystem =StoryBoardClickSystem.GetInstance();
        _dialogueTextAnimationManager =DialogueTextAnimationManager.GetInstance();
        _storyBoardClickSystem.SubscribeUiCheckClick(CheckClick);
    }
    
    private void CheckClick(RaycastHit2D hit)
    {
        if (hit.transform == this.transform)
        {
            if (!isOn)
            {
                isOn = true;
                this.gameObject.GetComponent<Image>().sprite = _imageList[1];
                _dialogueTextAnimationManager.ChangeFadeSpeed(0.24f);
            }
            else
            {
                isOn = false;
                this.gameObject.GetComponent<Image>().sprite = _imageList[0];
                _dialogueTextAnimationManager.ChangeFadeSpeed(0.08f);
            }
        }
    }
}
