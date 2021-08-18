using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//
// For changing the timescale, pausing and continuing the game
//

public class SimulationSpeedManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [Header("Simulation control buttons")]
    [SerializeField] Button play;
    [SerializeField] Button pause;
    [SerializeField] Button decrease;
    [SerializeField] Button increase;
    
    private bool isPaused;
    private float timeScale;
    private int roundedTimeScale;
    private float timeScaleBeforePause;

    void Update()
    {
        timeScale = Time.timeScale;
        roundedTimeScale = Mathf.RoundToInt(timeScale);

        if(Input.GetKeyDown(KeyCode.Q))
            DecreaseTimeScale();
        if(Input.GetKeyDown(KeyCode.E))
            IncreaseTimeScale();

        text.text = roundedTimeScale.ToString();
    }

    public void DecreaseTimeScale()
    {
        if(roundedTimeScale > 0 && !isPaused)
            Time.timeScale--;
        if(Time.timeScale == 0)
            decrease.gameObject.SetActive(false);
    }
    public void IncreaseTimeScale()
    {
        if(roundedTimeScale < 10 && !isPaused)
            Time.timeScale++;
        if(Time.timeScale >= 0)
            decrease.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        isPaused = true;
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;

        increase.gameObject.SetActive(false);
        decrease.gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = timeScaleBeforePause;

        increase.gameObject.SetActive(true);
        if(Time.timeScale > 0)
            decrease.gameObject.SetActive(true);
    }
}
