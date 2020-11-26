using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool isFiring;
    [SerializeField] ParticleSystem[] muzzleFlash;
    [SerializeField] ParticleSystem hitEffects;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] Transform raycastDestination;
    public Weapon w;
    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;

    public void StartShooting()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireWeapon();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1f / w.fireRate;
        while(accumulatedTime >-0f)
        {
            FireWeapon();
            accumulatedTime -= fireInterval;
        }
    }

    public void FireWeapon()
    {
        foreach (var particle in muzzleFlash)
        {
            if (w.curClipAmount <= 0)
            {
                w.curClipAmount = 0;
                return;
            }
            else
            {
                w.curAmmo -= 1;
                w.curClipAmount -= 1;
                particle.Emit(1);
            }
        }

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;
        if (Physics.Raycast(ray, out hitInfo))
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
