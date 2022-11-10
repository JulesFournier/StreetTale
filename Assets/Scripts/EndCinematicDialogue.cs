using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class EndCinematicDialogue : MonoBehaviour
{
    public DialogueHolder dialogue;
    public SwitchScene switcher;
    public string scene;
    
    void Update()
    {
        if (dialogue.isFinished)
        {
            switcher.switchScene(scene);
        }
    }
}
