using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Animal : MonoBehaviour, IKillable, IDamageable, IHasHunger, ICanMove
{
    // Basic stats
    [SerializeField] private float health = 100;
    [SerializeField] private Slider health_bar;
    [SerializeField] private float energy_storages = 100;
    [SerializeField] private Slider hunger_bar;
    [SerializeField] private float hunger_drain_interval = 1;
    private float time_for_hunger_decrease;

    // Movement variables
    private Rigidbody rb;
    private float distance_to_ground;
    [SerializeField] private Vector3 movement_target;
    [SerializeField] private float jump_strength = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distance_to_ground = GetComponent<Collider>().bounds.extents.y;
    }


    void FixedUpdate()
    {
        if (Time.time > time_for_hunger_decrease)
        {
            time_for_hunger_decrease = Time.time + hunger_drain_interval;
            Consume(1);
        }

        // Update health bars
        health_bar.value = health;
        hunger_bar.value = energy_storages;

        Move();
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }

    public void AddDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Feed(float kilocalories)
    {
        // TODO: Play feeding sound/animation/effect
        energy_storages += kilocalories;
    }

    public void Consume(float kilocalories)
    {
        if (energy_storages > 0)
        {
            energy_storages -= kilocalories;
        }
        else
        {
            AddDamage(kilocalories);
        }
    }

    public void MoveToDirection(Vector3 movement_direction)
    {
        throw new System.NotImplementedException();
    }

    public void MoveTowardsCoordinates(Vector3 target_coordinates)
    {
        movement_target = target_coordinates;
    }

    private void Move()
    {
        if (IsGrounded() && Vector3.Distance(transform.position, movement_target) > 1)
        {
            Vector3 jump_direction = Vector3.Normalize(movement_target - transform.position);
            jump_direction = Vector3.Normalize(jump_direction + Vector3.up);
            rb.velocity = jump_direction * jump_strength;
            Consume(1);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distance_to_ground + 0.1f);
    }
}
