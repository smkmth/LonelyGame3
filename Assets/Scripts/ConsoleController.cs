using UnityEngine;

using System;
using System.Collections.Generic;


public class ConsoleCommand
{


}
public class ConsoleController
{
   
    public ConsoleController(Dictionary<string, ConsoleCommand> commands)
    {
        if (_instance != null)
        {
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private static ConsoleController _instance;

    public static ConsoleController instance
    {
        get
        {
            if (_instance == null)
            {
                new ConsoleCommand();
            }

            return _instance;
        }
    }


    public Dictionary<string, ConsoleCommand> commands;

    public void Parse(string command)
    {
        ConsoleCommand thisCommand;
        commands.TryGetValue(command, out thisCommand);
    }
}
