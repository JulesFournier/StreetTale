using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header("Text options")]
        private Text textHolder;
        [SerializeField]private string input;
        [SerializeField]private Color textColor;
        [SerializeField]private Font textFont;
        
        [Header("Time parameters")]
        [SerializeField]private float delayBetweenLetters;
        [SerializeField]private float delayBetweenWords;
        [SerializeField]private float delayUntilNextLine;

        [Header("Sound parameters")] 
        [SerializeField]private AudioClip sound;
        
        [Header("Character image")] 
        [SerializeField]private Sprite characterSprite;
        [SerializeField]private Image imageHolder;
        
        private AllDialogues allDialogues;
        private IEnumerator lineAppear;

        private void Awake()
        {
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, textColor, textFont, delayBetweenWords, sound, delayUntilNextLine, delayBetweenLetters);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            allDialogues = GameObject.Find("AllDialogues").GetComponent<AllDialogues>();
            if (Input.GetMouseButtonDown(0) && !allDialogues.pausePanel.activeSelf)
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                    Finished = true;
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            Finished = false;
        }
    }
}