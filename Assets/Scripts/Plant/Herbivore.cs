using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : MonoBehaviour, IEdible, IKillable, IDamageable
{
    [SerializeField] Animal animal;
    [SerializeField] float health;
    const float max_nutrition = 100;
    [SerializeField] float nutrition;
    const float growth_interval = 1;
    [SerializeField] float next_growth_time;
    //[SerializeField] Animator animator;

    void Start()
    {
        nutrition = animal.health;
        next_growth_time = growth_interval + Random.Range(-growth_interval, growth_interval);
    }

    void FixedUpdate()
    {
        if (nutrition < max_nutrition && Time.time > next_growth_time)
        {
            next_growth_time += growth_interval + Random.Range(-growth_interval, growth_interval);
            nutrition += 1;
            UpdateAnimator();
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
}
