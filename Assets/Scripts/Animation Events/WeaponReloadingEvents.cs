using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponReloadingEvents : MonoBehaviour
{
    public Weapon w;
    public AudioSource weaponSounds;

    private void Start()
    {
        weaponSounds = GetComponent<AudioSource>();
    }

    public void StartReload()
    {
        IKController.instance.leftHandWeight = 0f;
    }

    public void StopReload()
    {
        IKController.instance.leftHandWeight = 1f;
    }

    public void MagOut()
    {
        weaponSounds.PlayOneShot(w.magOut, 0.7f);
    }

    public void MagIn()
    {
        weaponSounds.PlayOneShot(w.magIn, 0.7f);
    }

    public void CockWeapon()
    {
        weaponSounds.PlayOneShot(w.cockWeapon, 0.7f);
    }
}
