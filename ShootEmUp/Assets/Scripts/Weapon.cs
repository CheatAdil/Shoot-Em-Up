using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] public GameObject ammo;
    [SerializeField] public float projectile_speed;
    [SerializeField] public float damage;
    [SerializeField] public float rpm = 1;
}
public enum WeaponMode 
{
    elso,
    masodik,
    harmadik,
}
