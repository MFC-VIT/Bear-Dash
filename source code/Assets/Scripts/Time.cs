using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI element
    private float elapsedTime = 0f; // Variable to store the elapsed time

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Convert the elapsed time to a formatted string (minutes:seconds)
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");

        // Update the TextMeshProUGUI text
        timerText.text = minutes + ":" + seconds;
    }
}