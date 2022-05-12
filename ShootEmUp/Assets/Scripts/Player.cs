using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Weapon weapon;
    private SliderPackage current_package;
    Vector3 control;
    Vector3 prevPos;
    
    private float timer;
    private bool canShoot = true;

    [SerializeField] private float default_max_hp;
    
    private void Update()
    {
        prevPos = transform.position;
        control = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += Vector3.Normalize(control) * speed * Time.deltaTime;
        velocity = (transform.position - prevPos) / Time.deltaTime;
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > (60f/ weapon.GetWeapons()[((int)current_package.mode)].rpm))
            {
                timer = 0;
                canShoot = true;
            }
        }
        else 
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        switch (weapon.GetWeapons()[((int)current_package.mode)].type)
        {
            case WeaponType.single:
                {
                    if (weapon.GetWeapons()[((int)current_package.mode)] == null) return;
                    GameObject b = Instantiate(weapon.GetWeapons()[((int)current_package.mode)].ammo, transform.GetChild(0).position, Quaternion.identity);
                    ammo a = b.AddComponent<ammo>();
                    a.setup(weapon.GetWeapons()[((int)current_package.mode)].damage, weapon.GetWeapons()[((int)current_package.mode)].projectile_speed, new Vector3(1,0,0));
                    break;
                }
            case WeaponType.spread: 
                {
                    if (weapon.GetWeapons()[((int)current_package.mode)] == null) return;
                    float angIncr = weapon.GetWeapons()[((int)current_package.mode)].angle / weapon.GetWeapons()[((int)current_package.mode)].projectile_num;
                    float ang = weapon.GetWeapons()[((int)current_package.mode)].angle / 2f;
                    for (int i = 0; i < weapon.GetWeapons()[((int)current_package.mode)].projectile_num; i++) 
                    {
                        GameObject b = Instantiate(weapon.GetWeapons()[((int)current_package.mode)].ammo, transform.GetChild(0).position, Quaternion.identity);
                        ammo a = b.AddComponent<ammo>();
                        a.setup(weapon.GetWeapons()[((int)current_package.mode)].damage, weapon.GetWeapons()[((int)current_package.mode)].projectile_speed, new Vector3(Mathf.Cos(Mathf.Deg2Rad * ang), Mathf.Sin(Mathf.Deg2Rad * ang), 0));
                        ang -= angIncr;
                    }
                    break;
                }
            case WeaponType.mult:
                {
                    if (weapon.GetWeapons()[((int)current_package.mode)] == null) return;
                    float RangeIncr = weapon.GetWeapons()[((int)current_package.mode)].range * 2f / weapon.GetWeapons()[((int)current_package.mode)].projectile_num;
                    float pos = weapon.GetWeapons()[((int)current_package.mode)].range / 2f;
                    for (int i = 0; i < weapon.GetWeapons()[((int)current_package.mode)].projectile_num; i++)
                    {
                        GameObject b = Instantiate(weapon.GetWeapons()[((int)current_package.mode)].ammo, transform.GetChild(0).position + new Vector3(0 ,pos ,0 ), Quaternion.identity);
                        ammo a = b.AddComponent<ammo>();
                        a.setup(weapon.GetWeapons()[((int)current_package.mode)].damage, weapon.GetWeapons()[((int)current_package.mode)].projectile_speed, new Vector3(1,0,0));
                        pos -= RangeIncr;
                    }
                    break;
                }
        }
       canShoot = false;
    }
    private void RecieveSliderUpdate(SliderPackage package)
    {
        current_package = package;
        max_hp = NewMaxHP(package.setting);
        speed = (1f / (0.0014f *( package.setting * 100f + 47f)) + 1f);
    }
    private float NewMaxHP(float modifier)
	{
        float newMaxHP = default_max_hp * modifier;
        if (HP > newMaxHP) HP = newMaxHP;
        return newMaxHP;
    }
    public float GetSpeed() 
    {
        return speed;
    }
    public bool AtMaxHealth()
	{
        return (HP == max_hp);
	}
    public float MaxHealthPercentage()
	{
        return max_hp / default_max_hp;
	}
    public float CurrentHealthPercentage()
	{
        return HP / default_max_hp;
	}
}
