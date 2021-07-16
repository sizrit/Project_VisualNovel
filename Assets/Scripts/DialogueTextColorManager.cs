using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextColorManager : MonoBehaviour
{
    private GameObject _currentDialogueText;
    private GameObject _pastDialogueText;

    private void OnEnable()
    {
        _currentDialogueText = this.transform.GetChild(1).gameObject;
        _pastDialogueText = this.transform.GetChild(2).gameObject;
    }

    public void SetDialogueTextColor(string colorValue)
    {
        Color color = Color.white;
        switch (colorValue)
        {
            case "White":
                color=Color.white;
                break;
            
            case "Red":
                color =Color.red;
                break;
            
            case "Black":
                color = Color.black;
                break;
        }

        _currentDialogueText.GetComponent<Text>().color = color;
        _pastDialogueText.GetComponent<Text>().color = color;
    }
}
