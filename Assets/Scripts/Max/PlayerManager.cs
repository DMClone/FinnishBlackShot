using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    [SerializeField] private List<Camera> cameras = new();

    private Dictionary<Camera, PlayerInput> camerasLinkedToPlayers = new();
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeave;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= OnPlayerJoined;
        playerInputManager.onPlayerLeft -= OnPlayerLeave;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            Camera cam = cameras[i];
            if (!camerasLinkedToPlayers.ContainsKey(cam))
            {
                camerasLinkedToPlayers.Add(cam, input);
                input.gameObject.GetComponent<PlayerCamera>().playerCamera = cam;
                break;
            }
        }
    }
    public void OnPlayerLeave(PlayerInput input)
    {
        camerasLinkedToPlayers.Remove(input.gameObject.GetComponent<PlayerCamera>().playerCamera);
    }
}
