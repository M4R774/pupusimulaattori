using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationSpeedManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private float timeScale;
    private int roundedTimeScale;

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
        if(roundedTimeScale > 1)
            Time.timeScale--;
    }
    public void IncreaseTimeScale()
    {
        if(roundedTimeScale < 10)
            Time.timeScale++;
    }
}
