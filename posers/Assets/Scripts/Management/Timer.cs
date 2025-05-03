using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    // [SerializeField] private int remainingTime;

    [SerializeField] private UnityEvent onCountdownEnds;


    private Coroutine countdownCoroutine;

    public void StartCountdown(int totalSeconds)
    {   
        if (countdownCoroutine == null)
        {
            countdownCoroutine = StartCoroutine(Countdown(totalSeconds));
            countdownCoroutine = null;
        }
    }

    private IEnumerator Countdown(int totalSeconds)
    {
        int remainingTime = totalSeconds;

        while (remainingTime > 0)
        {
            int minutes = remainingTime / 60;
            int seconds = remainingTime % 60;

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        timerText.text = "00:00";
        onCountdownEnds?.Invoke();
    }
}
