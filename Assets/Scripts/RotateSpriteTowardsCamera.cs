using UnityEngine;

public class RotateSpriteTowardsCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
