using UnityEngine;

public class RotateSpriteTowardsCamera : MonoBehaviour
{
    void LateUpdate()
    {
        // TODO: Rotate only visible objects
        // TODO: Rotate only objects that are close enough
        // TODO: Dont rotate all of the objects at once if there are thousands objects visible
        // TODO: Instead of Update (or LateUpdate method), rotate them in a 
        // separate coroutine (minor performance boost, OnUpdate adds small overhead)
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
