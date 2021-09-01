using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    protected override void FixedUpdate()
    {
        if (reproduction_counter < max_reproduction_count && Time.time > next_reproduction_time)
        {
            Reproduce();
        }
    }
}
