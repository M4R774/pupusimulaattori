using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void AddDamage(float damage);
}

public interface IKillable
{
    public void Kill();
}

public interface IHasHunger
{
    public float GetFoodSaturationPercentage();
    public void Feed(float kilocalories);
    public void Consume(float kilocalories);
}

public interface ICanMove
{
    public void MoveToDirection(Vector3 movement_direction);
    public void MoveTowardsCoordinates(Vector3 target_coordinates);
}

public interface IEdible
{
    public float Eat(float bite_size);
    public bool HasSomethingToEat();
    public float GetNutritionPercentage();
    public float GetNutritionValue();
    public Vector3 GetPosition();
    public int GetID();
}