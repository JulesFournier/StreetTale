using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void openSettings()
    {
        settingsMenu.SetActive(true);
    }
}
