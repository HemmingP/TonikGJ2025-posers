using System.Collections;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusicSource;

    [SerializeField] private AudioClip roundEnded;
    [SerializeField] private AudioClip gameEnded;

    [SerializeField] private float delayBetweenPlays = 0.5f;


    private void PlaySound(AudioClip clip)
    {
        backgroundMusicSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();

        StopCoroutine(WaitUntilSongStartsAgain());
        StartCoroutine(WaitUntilSongStartsAgain());
    }

    public void PlayRoundEnded()
    {
        PlaySound(roundEnded);
    }


    public void PlayGameEnded()
    {
        PlaySound(gameEnded);
    }

    IEnumerator WaitUntilSongStartsAgain()
    {
        do
        {
            yield return new WaitForSeconds(delayBetweenPlays);
        } while (audioSource.isPlaying);

        backgroundMusicSource.Play();
    }
}
