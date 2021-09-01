using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoxAI : AnimalAI
{
    protected override GameObject GetNearbyFoodSource()
    {
        int layer_mask = 1 << 6; // Layer 6 = Animal
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
}