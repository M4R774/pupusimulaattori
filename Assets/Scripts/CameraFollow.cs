using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Space(10)]
    [Header("How to: Left mouse follow target, Right mouse free cam")]
    [SerializeField] bool isActive;
    [SerializeField] Camera cam = null;
    bool hasTarget = false;
    [SerializeField] Transform target = null;
    Transform origTransform;
    
    void Start()
    {
        if(cam != null)
        {
            cam = Camera.main;
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
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                
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
                cam.gameObject.transform.localPosition = origTransform.position;
            }

            if(hasTarget && target != null)
            {
                cam.gameObject.transform.localPosition = target.position + new Vector3(0,2.5f,-2.5f);
            }
            else if(hasTarget && target == null)
            {
                hasTarget = false;
                cam.gameObject.transform.localPosition = origTransform.position;
            }
        }
    }
}
