using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] bool isFiring;
    [SerializeField] ParticleSystem[] muzzleFlash;
    [SerializeField] ParticleSystem hitEffects;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] Transform raycastDestination;
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
        ray.direction = raycastDestination.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            hitEffects.transform.position = hitInfo.point;
            hitEffects.transform.forward = hitInfo.normal;
            hitEffects.Emit(1);
        }
    }

    public void StopShooting()
    {
        isFiring = false;
    }
}
