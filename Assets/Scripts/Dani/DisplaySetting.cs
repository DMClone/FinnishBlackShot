using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        List<string> optionsList = new List<string> { };
        for (int i = 0; i < Display.displays.Length - 1; i++)
            optionsList.Add("Display " + (i + 1));
        if (optionsList.Count > 0)
            dropdown.AddOptions(optionsList);

        if (PlayerPrefs.HasKey("PreferredDisplay"))
            dropdown.value = PlayerPrefs.GetInt("PreferredDisplay");
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetInt("PreferredDisplay", dropdown.value);
    }
}
