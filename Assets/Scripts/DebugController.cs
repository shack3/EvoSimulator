using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    public static DebugCommand<int> TIME_SCALE;

    public List<object> commandList;

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }
    public void OnReturn()
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }

    }

    private void Awake()
    {
        TIME_SCALE = new DebugCommand<int>("time.scale", "Sets the Time Scale", "time.scale", (x)=>
        {
            Time.timeScale = x;
        });

        commandList = new List<object>
        {
            TIME_SCALE,
        };
    }


    private void OnGUI()
    {
        if (!showConsole) { return; }

        GUI.Box(new Rect(0, 0, 300, 30), "");
        input = GUI.TextField(new Rect(10f, 5f, 300 - 20f, 20f), input);

    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for(int i=0; i<commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }           
        }
    }
}
