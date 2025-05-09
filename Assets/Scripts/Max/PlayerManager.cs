using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; } // Singleton instance

    public PlayerInputManager playerInputManager;

    [SerializeField] private List<Camera> cameras = new();

    private Dictionary<Camera, PlayerInput> camerasLinkedToPlayers = new();

    private int players = 0;
    [SerializeField] private TMPro.TextMeshProUGUI playersText;
    
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple PlayerManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        GameObject player = input.gameObject;
        for (int i = 0; i < cameras.Count; i++)
        {
            Camera cam = cameras[i];
            if (!camerasLinkedToPlayers.ContainsKey(cam))
            {
                player.transform.position = cam.transform.position;
                player.transform.parent = cam.transform;
                player.transform.localRotation = Quaternion.Euler(0, 0, 0);

                camerasLinkedToPlayers.Add(cam, input);
                player.GetComponent<PlayerCamera>().playerCamera = cam;
                player.GetComponent<DrawCards>().canvas.worldCamera = cam;
                break;
            }
        }
        players++;
        playersText.text = "Players Joined: " + players.ToString();
        if (players >= 2)
        {
            cameras[0].transform.GetChild(0).gameObject.GetComponent<EyeFollow>().otherPlayerTrans = cameras[1].transform;
            cameras[1].transform.GetChild(1).gameObject.GetComponent<EyeFollow>().otherPlayerTrans = cameras[0].transform;
        }
    }

    public void OnPlayerLeave(PlayerInput input)
    {
        camerasLinkedToPlayers.Remove(input.gameObject.GetComponent<PlayerCamera>().playerCamera);
    }

    public bool EnoughPlayersJoined()
    {
        return camerasLinkedToPlayers.Count >= 2;
    }
}