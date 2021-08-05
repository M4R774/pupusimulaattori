using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Animal : MonoBehaviour, IKillable, IDamageable, IHasHunger, ICanMove
{
    // Basic stats
    //[SerializeField] private float health = 100;
    public float health {get; private set;} = 100;
    [SerializeField] private Slider health_bar;
    [SerializeField] private float max_energy_storages = 100;
    [SerializeField] private float energy_storages = 100;
    [SerializeField] private Slider hunger_bar;
    [SerializeField] private float hunger_drain_interval = 10;
    public float vision_range = 10;
    private float time_for_hunger_decrease;

    // Movement variables
    private Rigidbody rb;
    private float distance_to_ground;
    [SerializeField] private Vector3 movement_target;
    [SerializeField] private float jump_strength = 3;
    [SerializeField] private float jump_cooldown;
    private float next_reproduction_time;

    void Awake()
    {
        next_reproduction_time = Time.time + 60;
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

        if (GetFoodSaturationPercentage() > 95 && Time.time > next_reproduction_time)
        {
            Reproduce();
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

    public void EatFromFoodSource(IEdible food_source)
    {
        Feed(food_source.Eat(1));
    }

    public void Feed(float kilocalories)
    {
        // TODO: Play feeding sound/animation/effect
        // TODO: Checks for overfeeding
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
        if (IsGrounded() && Vector3.Distance(transform.position, movement_target) > .5f && Time.time > jump_cooldown)
        {
            jump_cooldown += .5f;
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

    public float GetFoodSaturationPercentage()
    {
        return energy_storages / max_energy_storages * 100;
    }

    private void Reproduce()
    {
        try
        {
            Vector3 suitable_spawning_location = FindSuitableSpawningLocation();
            GameObject child = Instantiate(gameObject, suitable_spawning_location, Quaternion.identity);
        }
        catch (NoSuitableSpawnLocationFound)
        {

        }
        next_reproduction_time = Time.time + 60 + UnityEngine.Random.Range(-5f, 5f);
    }

    private Vector3 FindSuitableSpawningLocation()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 random_element = new Vector3(UnityEngine.Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawn_location_candidate = random_element + transform.position;
            int layer_mask = 1 << 6;  // Layer 6 = Animals
            Collider[] edibles_in_spawn_location_candidate = Physics.OverlapSphere(spawn_location_candidate, 1, layer_mask, QueryTriggerInteraction.Collide);
            if (edibles_in_spawn_location_candidate.Length > 0)
            {
                continue;
            }
            else
            {
                return spawn_location_candidate;
            }
        }
        throw (new NoSuitableSpawnLocationFound("No suitable spawn location found."));
    }
}
