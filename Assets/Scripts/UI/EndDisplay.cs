using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDisplay : MonoBehaviour
{
    Animator animator;

    public Text gameOverText;
    public Text winText;
    public Text timePassedText;

    public GameObject menuButton;
    public GameObject replayButton;
    public GameObject quitButton;
    public GameObject nextLevelButton;

    public TimeManager timeManager;

    public void DisplayWin()
    {
        animator = GetComponent<Animator>();

        winText.gameObject.SetActive(true);
        nextLevelButton.SetActive(true);

        animator.SetTrigger("Win");
        SetDisplay();
    }

    public void DisplayGameOver()
    {
        animator = GetComponent<Animator>();

        gameOverText.gameObject.SetActive(true);
    
        animator.SetTrigger("GameOver");
        SetDisplay();
    }

    private void SetDisplay()
    {
        timePassedText.text = "Time survived: " + timeManager.timePassed.ToString("f2");
    }
}
