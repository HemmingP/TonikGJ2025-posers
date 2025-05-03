using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private int totalSeconds = 60; // Editable in Inspector


    public void TriggerTimer()
    {
        if (timer != null)
        {
            timer.StartCountdown(totalSeconds);
        }
    }
}
