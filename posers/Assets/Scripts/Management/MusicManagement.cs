using System.Collections;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusicSource;

    [SerializeField] private AudioClip roundEnded;
    [SerializeField] private AudioClip gameEnded;

    [SerializeField] private float delayBetweenPlays = 0.5f;
    [SerializeField] private float roundedEndedDelay = 0.7f;


    private void Start()
    {
        StartCoroutine(WaitUntilSongStartsAgain(delayBetweenPlays));
    }

    private void PlaySound(AudioClip clip, float delay)
    {
        backgroundMusicSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();

        StopAllCoroutines();
        StartCoroutine(WaitUntilSongStartsAgain(delay));
    }

    public void PlayRoundEnded()
    {
        PlaySound(roundEnded, roundedEndedDelay);
    }


    public void PlayGameEnded()
    {
        PlaySound(gameEnded, delayBetweenPlays);
    }

    IEnumerator WaitUntilSongStartsAgain(float delay)
    {
        do
        {
            yield return new WaitForSeconds(delay);
        } while (audioSource.isPlaying);

        backgroundMusicSource.Play();
    }
}
