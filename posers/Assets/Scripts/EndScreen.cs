using System.Collections;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI textWinner;
    public GameObject anyKeyGO;
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
}
