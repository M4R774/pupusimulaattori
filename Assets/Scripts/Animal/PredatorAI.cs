using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

 [Obsolete("Method is obsolete.", true)]
public abstract class PredatorAI : MonoBehaviour
{
    [SerializeField] Animal animal;
    [SerializeField] Status status;

    // Idle movement
    private Rigidbody rb;
    private const float movement_radius = 5;
    private const float movement_cd = 10;
    private float next_idle_movement_time;

    // Food movement
    private GameObject food_target;
    private float next_food_movement_time;
    private float food_consumption_on_movement = 1.2f;

    enum Status
    {
        fleeing,
        eating,
        attacking,
        moving_towards_food,
        chasing,
        looking_for_food,
        idle
    }

    private void Start()
    {
        animal = GetComponent<Animal>();
        status = Status.idle;
    }

    private void FixedUpdate() 
    {
        // Reason for this to be in FixedUpdate?
        switch (status) 
        {
            case Status.fleeing:
                Flee();
                break;
            case Status.eating:
                Eat();
                break;
            case Status.attacking:
                Attack();
                break;
            case Status.moving_towards_food:
                MoveTowardsFood();
                break;
            case Status.chasing:
                Chase();
                break;
            case Status.looking_for_food:
                LookForFood();
                break;
            case Status.idle:
                Idle();
                break;
            default:
                Idle();
                break;
        }
    }

    private void Chase()
    {
        throw new NotImplementedException();
    }

    private void Flee()
    {
        throw new NotImplementedException();
    }

    private void Eat()
    {
        if (animal.GetFoodSaturationPercentage() >= 100)
        {
            status = Status.idle;
            next_idle_movement_time = Time.time + 1 + UnityEngine.Random.Range(0, 5);
        }
        else if (food_target == null || food_target.GetComponent<IEdible>().GetNutritionPercentage() <= 0) // or here fixes error when food_target (rabbit gameobject) is destroyed
        {
            status = Status.looking_for_food;
        }
        else if(food_target != null)
        {
            animal.EatFromFoodSource(food_target.GetComponent<IEdible>());
        }
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void LookForFood()
    {
        GameObject nearby_food_source = GetNearbyFoodSource();
        if (nearby_food_source != null)
        {
            food_target = nearby_food_source;
            status = Status.moving_towards_food;
        }
        else
        {
            // TODO: more efficient search pattern
            Idle();
        }
    }

    private GameObject GetNearbyFoodSource()
    {
        int layer_mask = 1 << 6;  // Layer 6 = Herbivore
        Collider[] edibles_in_vision_range = Physics.OverlapSphere(transform.position, animal.vision_range, layer_mask, QueryTriggerInteraction.Collide);
        List<Collider> sorted_edibles_list = edibles_in_vision_range.OrderByDescending(edible => CalculateFoodSourceAttractiviness(edible.GetComponent<IEdible>())).ToList();
        foreach (Collider edible_collider in sorted_edibles_list)
        {
            if (edible_collider.GetComponent<IEdible>().HasSomethingToEat())
            {
                return edible_collider.gameObject;
            }
        }
        // TODO: return most attractive food source
        return null;
    }

    private float CalculateFoodSourceAttractiviness(IEdible food_source)
    {
        float distance_to_food_source = Vector3.Distance(transform.position, food_source.GetPosition());
        return food_source.GetNutritionValue() - distance_to_food_source * food_consumption_on_movement;
    }

    private void MoveTowardsFood()
    {
        if (Time.time > next_food_movement_time)
        {
            next_food_movement_time += 1;
            if (food_target != null)
            {
                animal.MoveTowardsCoordinates(food_target.GetComponent<IEdible>().GetPosition());
            }
            else
            {
                status = Status.looking_for_food;
                return;
            }
            if (Vector3.Distance(transform.position, food_target.GetComponent<IEdible>().GetPosition()) < 1f)
            {
                status = Status.eating;
            }
        }
    }

    private void Idle()
    {
        if (Time.time > next_idle_movement_time)
        {
            next_idle_movement_time += movement_cd + UnityEngine.Random.Range(-movement_cd, movement_cd);
            Vector3 movement_vector = new Vector3(
                UnityEngine.Random.Range(-movement_radius, movement_radius), // x
                0,                                                           // y
                UnityEngine.Random.Range(-movement_radius, movement_radius)  // z
            );
            Vector3 movement_target = transform.position + movement_vector;
            animal.MoveTowardsCoordinates(movement_target);
        }
        if (animal.GetFoodSaturationPercentage() < 80)
        {
            status = Status.looking_for_food;
        }
    }
    /*[SerializeField] Animal animal;
    [SerializeField] Status status;

    // Idle movement
    private Rigidbody rb;
    private const float movement_radius = 5;
    private const float movement_cd = 10;
    private float next_idle_movement_time;

    // Food movement
    private IEdible food_target;
    private float next_food_movement_time;

    enum Status
    {
        fleeing,
        eating,
        attacking,
        moving_towards_food,
        chasing,
        looking_for_food,
        idle
    }

    private void Start()
    {
        animal = GetComponent<Animal>();
        status = Status.idle;
    }

    private void FixedUpdate() 
    {
        // Reason for this to be in FixedUpdate?
        switch (status) 
        {
            case Status.fleeing:
                Flee();
                break;
            case Status.eating:
                Eat();
                break;
            case Status.attacking:
                Attack();
                break;
            case Status.moving_towards_food:
                MoveTowardsFood();
                break;
            case Status.chasing:
                Chase();
                break;
            case Status.looking_for_food:
                LookForFood();
                break;
            case Status.idle:
                Idle();
                break;
            default:
                Idle();
                break;
        }
    }

    private void Chase()
    {
        throw new NotImplementedException();
    }

    private void Flee()
    {
        throw new NotImplementedException();
    }

    private void Eat()
    {
        if (animal.GetFoodSaturation() >= 100)
        {
            status = Status.idle;
        }
        else if (food_target.GetNutritionPercentage() <= 0)
        {
            status = Status.looking_for_food;
        }
        else
        {
            animal.EatFromFoodSource(food_target);
        }
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void LookForFood()
    {
        IEdible nearby_food_source = GetNearbyFoodSource();
        if (nearby_food_source != null)
        {
            food_target = nearby_food_source;
            status = Status.moving_towards_food;
        }
        else
        {
            // TODO: more efficient search pattern
            Idle();
        }
    }

    private IEdible GetNearbyFoodSource()
    {
        int layer_mask = 1 << 6;  // Layer 6 = Herbivore
        Collider[] edibles_in_vision_range = Physics.OverlapSphere(transform.position, animal.vision_range, layer_mask, QueryTriggerInteraction.Collide);
        List<Collider> sorted_edibles_list = edibles_in_vision_range.OrderByDescending(edible => edible.GetComponent<IEdible>().Attractiveness()).ToList();
        foreach (Collider edible_collider in sorted_edibles_list)
        {
            if (edible_collider.GetComponent<IEdible>().HasSomethingToEat())
            {
                return edible_collider.GetComponent<IEdible>();
            }
        }
        // TODO: return most attractive food source
        return null;
    }

    private void MoveTowardsFood()
    {
        if (Time.time > next_food_movement_time)
        {
            next_food_movement_time += 1;
            animal.MoveTowardsCoordinates(food_target.GetPosition());
            
            if (Vector3.Distance(transform.position, food_target.GetPosition()) < 1f)
            {
                status = Status.eating;
            }
        }
    }

    private void Idle()
    {
        if (Time.time > next_idle_movement_time)
        {
            next_idle_movement_time += movement_cd + UnityEngine.Random.Range(-movement_cd, movement_cd);
            Vector3 movement_vector = new Vector3(
                UnityEngine.Random.Range(-movement_radius, movement_radius), // x
                0,                                                           // y
                UnityEngine.Random.Range(-movement_radius, movement_radius)  // z
            );
            Vector3 movement_target = transform.position + movement_vector;
            animal.MoveTowardsCoordinates(movement_target);
        }
        if (animal.GetFoodSaturation() < 80)
        {
            status = Status.looking_for_food;
        }
    }*/
}
