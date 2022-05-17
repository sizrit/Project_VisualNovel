using System.Collections;
using System.Collections.Generic;
using ClickSystem;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardFastForwardButton : MonoBehaviour
{
    [SerializeField] private bool isOn = false;
    readonly List<Sprite> _imageList = new List<Sprite>();

    private void Start()
    {
        string loadPath = "Images/UI_Button/";
        _imageList.Add(Resources.Load<Sprite>(loadPath+"FFoff"));
        _imageList.Add(Resources.Load<Sprite>(loadPath+"FFon"));
        this.gameObject.GetComponent<Image>().sprite = _imageList[0];
        
        StoryBoardClickSystem.GetInstance().SubscribeUiCheckClick(CheckClick);
    }

    private void CheckClick(RaycastHit2D hit)
    {
        if (hit.transform == this.transform)
        {
            if (!isOn)
            {
                isOn = true;
                this.gameObject.GetComponent<Image>().sprite = _imageList[1];
                DialogueTextAnimationManager.GetInstance().ChangeFadeSpeed(0.24f);
            }
            else
            {
                isOn = false;
                this.gameObject.GetComponent<Image>().sprite = _imageList[0];
                DialogueTextAnimationManager.GetInstance().ChangeFadeSpeed(0.08f);
            }
        }
    }
}
