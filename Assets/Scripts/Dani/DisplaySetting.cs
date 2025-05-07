using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown.ClearOptions();
        List<string> optionsList = new List<string> { "Splitscreen" };
        for (int i = 0; i < Display.displays.Length - 1; i++)
            optionsList.Add("Display " + (i + 1));
        dropdown.AddOptions(optionsList);

        if (PlayerPrefs.HasKey("PreferredDisplay"))
            dropdown.value = PlayerPrefs.GetInt("PreferredDisplay");
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetInt("PreferredDisplay", dropdown.value);
    }
}
