using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private int totalSeconds = 60; // Editable in Inspector


    public void ReduceTotalSeconds(int seconds)
    {
        totalSeconds = Mathf.Max(5, totalSeconds - seconds);
    }

    public void TriggerTimer()
    {
        if (timer != null)
        {
            timer.StartCountdown(totalSeconds);
        }
    }
}
