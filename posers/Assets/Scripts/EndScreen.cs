using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI textWinner;
    public void Toggle()
    {
        GetComponent<Animator>().SetTrigger("Toggle Endscreen");
    }

    public void WriteWinner(string text)
    {
        textWinner.text = text;
    }   
}
