using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip weaponPull;
    public AudioClip explosionSound;
    public AudioClip music;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playEngineSound());
    }
 
    IEnumerator playEngineSound()
    {
        audioSource.PlayOneShot(weaponPull);
        yield return new WaitForSeconds(weaponPull.length - 0.7f);
        audioSource.loop = true;
        audioSource.clip = music;
        audioSource.Play();
    }

    public void Pause()
    {
        if (!audioSource)
            return;

        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();
    }

    public IEnumerator StopBossMusic(AudioClip musicToPlay)
    {
        yield return new WaitForSeconds(0.5f);
        while (audioSource.volume > 0) {
            audioSource.volume -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(explosionSound);
        yield return new WaitForSeconds(explosionSound.length - 0.5f);
        audioSource.PlayOneShot(musicToPlay);
    }
}