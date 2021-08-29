using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Herbivore : MonoBehaviour, IEdible, IKillable, IDamageable
{
    [SerializeField] Animal animal;
    public const int id = 0;
    [SerializeField] float health;
    const float max_nutrition = 100;
    [SerializeField] float nutrition;
    const float growth_interval = 2;
    [SerializeField] float next_growth_time;
    //[SerializeField] Animator animator;

    // Reproduction
    private int reproduction_counter;
    private const int max_reproduction_count = 2;
    private float next_reproduction_time;
    private const float reproduction_interval = 60;

    void Awake()
    {
        nutrition = animal.health;
        reproduction_counter = 0;
        next_reproduction_time = Time.time + 10;
        next_growth_time = growth_interval + UnityEngine.Random.Range(-growth_interval, growth_interval);
    }

    void FixedUpdate()
    {
        if (nutrition < max_nutrition && Time.time > next_growth_time)
        {
            next_growth_time = Time.time + growth_interval + UnityEngine.Random.Range(-growth_interval, growth_interval);
            nutrition += 1;
            UpdateAnimator();
        }
        if (reproduction_counter < max_reproduction_count && Time.time > next_reproduction_time)
        {
            Reproduce();
        }
    }

    public float Eat(float bite_size)
    {
        nutrition -= bite_size;
        animal.AddDamage(1f);
        UpdateAnimator();
        return bite_size;
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

    private void AddNutrition(float nutrition_to_be_added)
    {
        nutrition += nutrition_to_be_added;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        float nutrition_percentage = nutrition / max_nutrition * 100;
        //animator.SetFloat("nutrition_percentage", nutrition_percentage);
    }

    public bool HasSomethingToEat()
    {
        float nutrition_percentage = nutrition / max_nutrition * 100;
        if (nutrition_percentage > 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float Attractiveness()
    {
        return GetNutritionPercentage();
    }

    public float GetNutritionPercentage()
    {
        return nutrition / max_nutrition * 100;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public float GetNutritionValue()
    {
        return nutrition;
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
        reproduction_counter++;
        next_reproduction_time = Time.time + reproduction_interval + UnityEngine.Random.Range(-5f, 5f);
    }

    private Vector3 FindSuitableSpawningLocation()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 random_element = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, Random.Range(-5f, 5f));
            Vector3 spawn_location_candidate = random_element + transform.position;
            int layer_mask = 1 << 3;  // Layer 3 = Edibles
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

    public int GetID()
    {
        return id;
    }

    public class NoSuitableSpawnLocationFound: Exception
    {
        public NoSuitableSpawnLocationFound(string message): base(message) { }
    }
}
