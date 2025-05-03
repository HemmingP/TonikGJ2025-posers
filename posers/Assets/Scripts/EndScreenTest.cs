using UnityEngine;

public class EndScreenTest : MonoBehaviour
{
    public ToggleScreen toggleScreen;
    public float time = 4f;
    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if( currentTime >= time)
        {
            toggleScreen.Toggle();
            currentTime -= time;
        }
    }
}
