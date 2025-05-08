using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class ActivateDisplays : MonoBehaviour
{
    [SerializeField] private List<Camera> playerCameras = new();

    [SerializeField] private Camera startCamera;

    private void Start()
    {
        foreach(Camera cam in playerCameras)
        {
            cam.enabled = false;
        }
    }
    public void SetUpDisplays()
    {
        if (Display.displays.Length > 1)
        {
            // Multiple screens
            for (int i = 0; i < playerCameras.Count && i < Display.displays.Length; i++)
            {
                Display.displays[i].Activate();
                playerCameras[i].enabled = true;
                playerCameras[i].targetDisplay = i;
            }
        }
        else
        {
            // Splitscreen
            int cameraCount = playerCameras.Count;
            for (int i = 0; i < cameraCount; i++)
            {
                float width = 1f / cameraCount;
                playerCameras[i].enabled = true;
                playerCameras[i].rect = new Rect(i * width, 0, width, 1);
            }
        }
        startCamera.enabled = false;
    }
}