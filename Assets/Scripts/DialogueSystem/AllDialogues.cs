using System;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DialogueSystem
{
    public class AllDialogues : MonoBehaviour
    {
        public GameObject pausePanel;
        private void DeactivateAll()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void EnableDialogue(string dialogueName)
        {
            DeactivateAll();
            transform.Find(dialogueName).gameObject.SetActive(true);
        }
        
        public void EnableRandomizedDialogue(string dialogueName, int percentage)
        {
            if (Random.Range(0, 100) <= percentage)
            {
                DeactivateAll();
                transform.Find(dialogueName).gameObject.SetActive(true);
            }
        }
    }
}