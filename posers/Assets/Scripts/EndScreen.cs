using System.Collections;
using TMPro;
using UnityEngine;
public enum WinStatus{
    NO_ONE,
    ANGELO,
    FRAZ
}

[RequireComponent (typeof(AudioSource))]
public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI textWinner;
    public GameObject anyKeyGO;
    [SerializeField] private AudioClip[] angeloWinsSounds;
    [SerializeField] private AudioClip[] franzWinsSounds;
    [SerializeField] private AudioClip[] bothLooseSounds;
    private AudioSource audioSource => GetComponent<AudioSource>();
    public void Toggle()
    {
        GetComponent<Animator>().SetTrigger("Toggle Endscreen");
    }

    public void WriteWinner(string text)
    {
        textWinner.text = text;
    }   

    public void EnablePressAnyKey()
    {
        anyKeyGO.SetActive(true);
    }

    public void PlayEndSound(WinStatus winStatus)
    {
        if(winStatus == WinStatus.NO_ONE)
        {
            audioSource.resource = bothLooseSounds[Random.Range(0, bothLooseSounds.Length)];
        }
        if(winStatus == WinStatus.ANGELO)
        {
            audioSource.resource = angeloWinsSounds[Random.Range(0, angeloWinsSounds.Length)];
        }
        if(winStatus == WinStatus.FRAZ)
        {
            audioSource.resource = franzWinsSounds[Random.Range(0, franzWinsSounds.Length)];
        }
        audioSource.Play();
    }
}
