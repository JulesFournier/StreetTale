using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    public AudioManager music;
    public DialogueSoundManager dialog;

    public void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeInHierarchy)
            {
                settingsMenu.SetActive(false);
            } else
            {
                if (Time.timeScale == 1f)
                    Pause();
                else
                    Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        if (music)
            music.Pause();
        dialog.Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        if (music)
            music.Pause();
        dialog.Pause();
    }
}
