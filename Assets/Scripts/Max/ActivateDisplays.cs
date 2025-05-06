using UnityEngine;

public class ActivateDisplays : MonoBehaviour
{
    private void Start()
    {
        SetUpDisplays();
    }
    public void SetUpDisplays()
    {
        Debug.Log("Displays connected: " + Display.displays.Length);
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}