using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] private sub_weapon[] weapons;

    public sub_weapon[] GetWeapons()
    {
        return weapons;
    }
}
