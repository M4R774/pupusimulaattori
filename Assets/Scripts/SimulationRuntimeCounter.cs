using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationRuntimeCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private float timeFromStart;
    private int roundedTime;

    void Update()
    {
        timeFromStart = Time.time;
        roundedTime = Mathf.RoundToInt(timeFromStart);
        text.text = roundedTime.ToString();
    }
}
