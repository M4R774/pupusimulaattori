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
    public void Feed(float kilocalories);
    public void Consume(float kilocalories);
}