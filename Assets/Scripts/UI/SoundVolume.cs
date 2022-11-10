using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundVolume : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public AudioMixer audioMixer;
    public string audioName;

    // Start is called before the first frame update
    void Start()
    {
        float result = 0;
        audioMixer.GetFloat(audioName, out result);
        slider.value = result;
        result += 80;
        result = result * 100 / 80;
        int resultInt = (int)result;
        text.text = resultInt.ToString();
    }

    public void setTextValue(float volume)
    {
        float result = volume + 80;
        result = result * 100 / 80;
        int resultInt = (int) result;
        text.text = resultInt.ToString();
    }
}
