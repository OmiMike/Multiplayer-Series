using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Big Shot Studios/Inventory/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public float fireRate;
    public float damage;
    public int maxAmmo;
    public int curAmmo;
    public int maxClipAmount;
    public int curClipAmount;
}
