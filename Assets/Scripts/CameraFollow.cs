using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Space(10)]
    [Header("How to: Left mouse follow target, Right mouse free camera")]
    [SerializeField] bool isActive;
    [SerializeField] Camera camera = null;
    bool hasTarget = false;
    [SerializeField] Transform target = null;
    Transform origTransform;
    
    void Start()
    {
        if(camera != null)
        {
            camera = Camera.main;
        }
        origTransform = this.transform;
    }

    void Update()
    {
        if(isActive)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.gameObject.tag == "Animal")
                    {
                        target = hit.transform;
                        hasTarget = true;
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                target = null;
                hasTarget = false;
                camera.gameObject.transform.localPosition = origTransform.position;
            }

            if(hasTarget)
            {
                camera.gameObject.transform.localPosition = target.position + new Vector3(0,2.5f,-2.5f);
            }
        }
    }
}