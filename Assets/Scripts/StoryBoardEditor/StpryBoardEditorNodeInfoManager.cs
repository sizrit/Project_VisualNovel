using System.Collections;
using System.Collections.Generic;
using StoryBoardEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StpryBoardEditorNodeInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject storyinputField;

    public void ResetNodeInfoManager()
    {
        
    }

    public void CheckClick(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.transform.name == "Apply")
            {
                string nodeName = StoryBoardEditorClickSystem.GetInstance().GetCurrentSelectedNode().name;
                StoryBoardNode node = StoryBoardEditorNodeManager.GetInstance().GetNodeByName(nodeName);
                node.nodeId = this.GetComponentInChildren<InputField>().text;
                node.nodeObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshPro>().text =
                    node.nodeId;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
