using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeRemaining;
    private bool notPaused = true;
    private bool shiftOver = false;
    public Text timeText;

    // Start is called before the first frame update
    private void Update()
    {
        if (!shiftOver)//timer for the shift
        {
            if (timeRemaining > 0 && notPaused)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else if (notPaused)
            {
                Debug.Log("Your shift has ended!");
                timeRemaining = 0f;
                DisplayTime(timeRemaining);
                shiftOver = true;
                FindObjectOfType<GameManager>().GetComponent<GameManager>().EndOfDay();
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("Time Remaining in Your Shift: "+"{0:00}:{1:00}", minutes, seconds);
    }

    public void setPaused(bool newB)
    {
        notPaused = newB;
    }
}
