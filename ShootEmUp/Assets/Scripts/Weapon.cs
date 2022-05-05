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
    [SerializeField] public WeaponType type;
    ///if spread
    [SerializeField] public int projectile_num;
    [SerializeField] public float angle;

    //if mult
    [SerializeField] public float range;
    

}
public enum WeaponMode 
{
    elso,
    masodik,
    harmadik,
}
public enum WeaponType 
{
    single,
    spread,
    mult,
}
