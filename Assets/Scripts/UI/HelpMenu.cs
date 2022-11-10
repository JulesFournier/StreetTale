using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] GameObject helpMenu;

    void Start()
    {
        helpMenu.SetActive(false);
    }

    public void openHelp()
    {
        helpMenu.SetActive(true);
    }

    public void closeHelp()
    {
        helpMenu.SetActive(false);
    }
}
