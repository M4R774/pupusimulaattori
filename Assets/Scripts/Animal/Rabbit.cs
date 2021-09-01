using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal, IEdible
{
    public float Eat(float bite_size)
    {
        AddDamage(1);
        //UpdateAnimator();
        return bite_size;
    }
    public bool HasSomethingToEat()
    {
        float nutrition_percentage = energy_storages / max_energy_storages * 100;
        if (nutrition_percentage > 0.1f)
        {
            return true;
        }
        else
        {
            return false;   
        }
    }
    public float GetNutritionPercentage()
    {
        return energy_storages / max_energy_storages * 100;
    }
    public float GetNutritionValue()
    {
        return energy_storages;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public int GetID()
    {
        int id = 69;
        return id;
    }
}
