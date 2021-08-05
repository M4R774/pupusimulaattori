using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Simple camera movement. Left Shift to use vertical arrows as up + down
//

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Vector3 moveDirection = Vector3.zero;
    private float inputX;
    private float inputY;
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        if (inputX != 0)
            moveHorizontal();
        if (inputY != 0)
            if(Input.GetKey(KeyCode.LeftShift))
                moveVertical();
            else
                moveDepth();
    }
 
    void moveHorizontal()
    {
        transform.position += (new Vector3 (inputX * Time.deltaTime * speed, 0, 0));
    }
 
    void moveDepth()
    {
        transform.position += (new Vector3 (0, 0, inputY * Time.deltaTime * speed));
    }

    void moveVertical()
    {
        transform.position += (new Vector3 (0, inputY * Time.deltaTime * speed, 0));
    }
}
