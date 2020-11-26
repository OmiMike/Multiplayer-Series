using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    Ray ray;
    RaycastHit hitInfo;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        ray.origin = mainCam.transform.position;
        ray.direction = mainCam.transform.forward;
        if(Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = ray.origin + ray.direction * 1000f;
        }
    }
}
