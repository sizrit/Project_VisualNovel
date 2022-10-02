using System;
using System.Collections;
using System.Collections.Generic;
using StoryBoardEditor.NodeInfo;
using UnityEngine;
using UnityEngine.UI;

public class TestScrollScript : MonoBehaviour
{
    private List<string> myList = new List<string>();
    [SerializeField] private GameObject asd;
    [SerializeField] private GameObject textGameObject;
    private bool isOpen = false;
    
    private void OnEnable()
    {
        myList.Add("Content01");
        myList.Add("Content02");
        myList.Add("Content03");
        myList.Add("Content04");
        myList.Add("Content05");
        myList.Add("Content06");
        myList.Add("Content07");
        myList.Add("Content08");
        
    }

    private void GetString(string s)
    {
        isOpen = false;
        textGameObject.GetComponent<Text>().text = s;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

            foreach (var hit in hits)
            {
                if (hit.transform == textGameObject.transform)
                {
                    if (isOpen)
                    {
                        isOpen = false;
                        return;
                    }
                    isOpen = true;
                    GameObject obj = Instantiate(asd, this.transform);
                    obj.GetComponent<ScrollSelectionManager>().SetScrollSelection(myList,GetString);
                }
            }
        }
    }
}
