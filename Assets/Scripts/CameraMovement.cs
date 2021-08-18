using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Simple camera movement. Left Shift to use vertical arrows as up + down
// TO DO: Change camera movement to not use time - can be then used while game is pause & simulation speed doesn't affect it
//

public class CameraMovement : MonoBehaviour
{
    [Space(10)]
    [Header("How to: WASD, mouse wheel = y pos, shift = faster")]
    [SerializeField] float speed = 5f;
    [SerializeField] float scrollSensitivity = 5;
    [SerializeField] float shiftSpeedMultiplier = 5;
    private float userShiftSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private float inputX;
    private float inputZ;
    private float inputY;
    private bool inputShift;

    void Start()
    {
        userShiftSpeed = shiftSpeedMultiplier;
    }
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        inputY = Input.mouseScrollDelta.y;
        inputShift = Input.GetKey(KeyCode.LeftShift);

        if (inputX != 0)
            moveHorizontal();
        if (inputZ != 0)
            moveDepth();
        if(inputY != 0)
            moveVertical();
        if(inputShift)
            shiftSpeedMultiplier = userShiftSpeed;
        else
            shiftSpeedMultiplier = 1;

        if(transform.position.y <= 0.5)
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
 
    void moveHorizontal()
    {
        transform.position += (new Vector3 (inputX * Time.deltaTime * speed * shiftSpeedMultiplier, 0, 0));
    }
 
    void moveDepth()
    {
        transform.position += (new Vector3 (0, 0, inputZ * Time.deltaTime * speed * shiftSpeedMultiplier));
    }

    void moveVertical()
    {
        transform.position += (new Vector3 (0, inputY * Time.deltaTime * speed * scrollSensitivity * shiftSpeedMultiplier, 0));
    }
}
