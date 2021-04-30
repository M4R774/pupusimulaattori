using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalMovementController : MonoBehaviour
{
    [SerializeField] Animal animal;
    Status status;

    // Movement variables
    private Rigidbody rb;
    private const float movement_radius = 5;
    private const float movement_cd = 10;
    private float next_idle_movement_time;

    enum Status
    {
        idle, 
        looking_for_food, 
        fleeing,
        eating,
        chasing, 
        attacking
    }

    private void Start()
    {
        animal = GetComponent<Animal>();
        status = Status.idle;
    }

    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.idle:
                Idle();
                break;
            default:
                Idle();
                break;
        }
    }

    private void Idle()
    {
        if (Time.time > next_idle_movement_time)
        {
            Vector3 movement_vector = new Vector3(
                Random.Range(-movement_radius, movement_radius), // x
                0,                                               // y
                Random.Range(-movement_radius, movement_radius)  // z
            );
            Vector3 movement_target = transform.position + movement_vector;
            animal.MoveTowardsCoordinates(movement_target);
            next_idle_movement_time += movement_cd + Random.Range(-movement_cd, movement_cd);
        }
    }
}
