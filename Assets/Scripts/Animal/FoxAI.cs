using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoxAI : AnimalAI
{
    protected override GameObject GetNearbyFoodSource()
    {
        int layer_mask = 1 << 3;
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
}