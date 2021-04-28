using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    private float time_to_change_direction;
    private float change_direction_interval = 5;
    private Vector3 current_direction;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > time_to_change_direction)
        {
            time_to_change_direction += 5f;
            current_direction = Vector3.Normalize(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)));
        }
        transform.position = transform.position + current_direction/100;
    }
}
