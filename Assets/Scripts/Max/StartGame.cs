using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    // A list of actions (functions) to execute when the signal is sent
    //[SerializeField] private List<Action> functionsToExecute = new();

    public UnityEvent unityEvent;
    // Public function to send the signal and execute all registered functions
    public void ExecuteFunctions()
    {
        if (!PlayerManager.Instance.EnoughPlayersJoined()) return;
        unityEvent.Invoke();
    }
}
