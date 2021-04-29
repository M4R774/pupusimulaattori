using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Animal : MonoBehaviour, IKillable, IDamageable, IHasHunger
{
    // Basic stats
    [SerializeField] private float health = 100;
    [SerializeField] private Slider health_bar;
    [SerializeField] private float energy_storages = 100;
    [SerializeField] private Slider hunger_bar;
    [SerializeField] private float hunger_drain_interval = 1;
    private float time_for_hunger_decrease;

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
}
