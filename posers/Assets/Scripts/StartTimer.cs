using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private Timer timer;

    public void TriggerTimer()
    {
        if (timer != null)
        {
            timer.StartCountdown();
        }
    }
}
