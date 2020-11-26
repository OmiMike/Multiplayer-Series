using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] bool isFiring;
    [SerializeField] ParticleSystem[] muzzleFlash;
    [SerializeField] ParticleSystem hitEffects;
    [SerializeField] Transform raycastOrigin;
    Ray ray;
    RaycastHit hitInfo;

    public void StartShooting()
    {
        foreach(var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        isFiring = true;
        ray.origin = raycastOrigin.position;
        ray.direction = raycastOrigin.forward;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
        }
    }

    public void StopShooting()
    {
        isFiring = false;
    }
}
