using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public Timer timerScript; // Reference to the Timer script

    private void Start()
    {
        if (winCanvas != null)
            winCanvas.SetActive(true);
        if (loseCanvas != null)
            loseCanvas.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Police"))
        {
            Destroy(gameObject);

            if (winCanvas != null)
                winCanvas.SetActive(false);
            if (loseCanvas != null)
            {
                loseCanvas.SetActive(true);

                // Display "You Lose!" and the score
                TextMeshProUGUI loseText = loseCanvas.GetComponentInChildren<TextMeshProUGUI>();
                if (loseText != null && timerScript != null)
                {
                    loseText.text = "You Lose!\nScore: " + Mathf.FloorToInt(timerScript.GetElapsedTime());
                }
            }
        }
    }
}
