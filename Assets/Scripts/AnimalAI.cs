using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour, IDamageable, IKillable
{
    // Movement variables
    private Rigidbody rb;
    private float time_to_change_direction;
    private const float change_direction_interval = 1;

    // Basic stats
    [SerializeField] private float health;
    [SerializeField] private float hunger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100;
        hunger = 100;
    }

    void FixedUpdate()
    {
        if (Time.time > time_to_change_direction)
        {
            time_to_change_direction += change_direction_interval + Random.Range(-.1f, .1f);
            rb.velocity = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f))*3;
        }
    }

    public void Kill()
    {
        throw new System.NotImplementedException();
    }

    public void AddDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
