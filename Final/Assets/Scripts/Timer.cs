using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    private float startTime;

    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    // Method to get the elapsed time
    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }
}
