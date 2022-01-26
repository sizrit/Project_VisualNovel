using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryBoardNode : Node
{
    [SerializeField] private string dialogueId = "";

    [SerializeField] private BgId bgid;

    [SerializeField] private StoryBoardMode mode;

    [SerializeField] private string imgaeSetId;
    
    public void Initialize(Vector2 position)
    {
        dialogueId = "S0000";
        bgid = BgId.Chapter01Room;
        mode = StoryBoardMode.Dialogue;
        imgaeSetId = "ImageSet000";
        SetPosition(new Rect(position,Vector2.zero));
    }

    public void Draw()
    {
        TextField dialogueIdTextField = new TextField();
        dialogueIdTextField.value = dialogueId;
        titleContainer.Insert(0,dialogueIdTextField);

        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);
        
        Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        outputPort.portName = "Output";
        inputContainer.Add(outputPort);
    }
}
