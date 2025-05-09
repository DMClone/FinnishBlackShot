using System;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // A list of actions (functions) to execute when the signal is sent
    [SerializeField] private List<Action> functionsToExecute = new();

    // Public function to send the signal and execute all registered functions
    public void ExecuteFunctions()
    {
        foreach (var function in functionsToExecute)
        {
            function?.Invoke(); // Safely invoke the function
        }
    }
}
