using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool Finished { get; protected set; }
        protected IEnumerator WriteText(
            string input, 
            Text textHolder, 
            Color textColor, 
            Font textFont,
            float delayBetweenWords,
            AudioClip sound,
            float delayUntilNextLine,
            float delayBetweenLetters
        )
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                if (input[i] != ' ')
                {
                    yield return new WaitUntil(() => GameObject.Find("DialogueSoundManager") != null);
                    DialogueSoundManager.instance.PlaySound(sound);
                    yield return new WaitForSeconds(delayBetweenLetters);
                }
                else
                {
                    yield return new WaitForSeconds(delayBetweenWords);
                }
            }

            yield return new WaitForSeconds(delayUntilNextLine);
            // yield return new WaitUntil(() => Input.GetMouseButton(0));
            Finished = true;
        }
    }  
}

