using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Weapon[] weapon;
    private SliderPackage current_package;
    public Vector3 velocity;
    Vector3 control;
    Vector3 prevPos;
    
    private float timer;
    private bool canShoot = true;

    [SerializeField] private float default_max_hp;
    [SerializeField] private float max_hp;
    [SerializeField] private float HP;
    private void Update()
    {
        prevPos = transform.position;
        control = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += Vector3.Normalize(control) * speed * Time.deltaTime;
        velocity = (transform.position - prevPos) / Time.deltaTime;
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > (60f/ weapon[((int)current_package.mode)].rpm))
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
        switch (weapon[((int)current_package.mode)].type)
        {
            case WeaponType.single:
                {
                    if (weapon[((int)current_package.mode)] == null) return;
                    GameObject b = Instantiate(weapon[((int)current_package.mode)].ammo, transform.GetChild(0).position, Quaternion.identity);
                    ammo a = b.AddComponent<ammo>();
                    a.setup(weapon[((int)current_package.mode)].damage, weapon[((int)current_package.mode)].projectile_speed, new Vector3(1,0,0));
                    break;
                }
            case WeaponType.spread: 
                {
                    if (weapon[((int)current_package.mode)] == null) return;
                    float angIncr = weapon[((int)current_package.mode)].angle / weapon[((int)current_package.mode)].projectile_num;
                    float ang = weapon[((int)current_package.mode)].angle / 2f;
                    for (int i = 0; i < weapon[((int)current_package.mode)].projectile_num; i++) 
                    {
                        GameObject b = Instantiate(weapon[((int)current_package.mode)].ammo, transform.GetChild(0).position, Quaternion.identity);
                        ammo a = b.AddComponent<ammo>();
                        a.setup(weapon[((int)current_package.mode)].damage, weapon[((int)current_package.mode)].projectile_speed, new Vector3(Mathf.Cos(Mathf.Deg2Rad * ang), Mathf.Sin(Mathf.Deg2Rad * ang), 0));
                        ang -= angIncr;
                    }
                    break;
                }
            case WeaponType.mult:
                {
                    if (weapon[((int)current_package.mode)] == null) return;
                    float RangeIncr = weapon[((int)current_package.mode)].range * 2f / weapon[((int)current_package.mode)].projectile_num;
                    float pos = weapon[((int)current_package.mode)].range / 2f;
                    for (int i = 0; i < weapon[((int)current_package.mode)].projectile_num; i++)
                    {
                        GameObject b = Instantiate(weapon[((int)current_package.mode)].ammo, transform.GetChild(0).position + new Vector3(0 ,pos ,0 ), Quaternion.identity);
                        ammo a = b.AddComponent<ammo>();
                        a.setup(weapon[((int)current_package.mode)].damage, weapon[((int)current_package.mode)].projectile_speed, new Vector3(1,0,0));
                        pos -= RangeIncr;
                    }
                    break;
                    break;
                }
        }
       canShoot = false;
    }
    private void RecieveSliderUpdate(SliderPackage package)
    {
        current_package = package;
        max_hp = default_max_hp * package.setting;
        speed = (1f / (0.0014f *( package.setting * 100f + 47f)) + 1f);
    }
}
