using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Weapon weapon;
    public Vector3 velocity;
    Vector3 control;
    Vector3 prevPos;

     private float timer;
     private bool canShoot = true;

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
            if (timer > (60f/ weapon.rpm))
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
       if (weapon == null) return;
       GameObject b = Instantiate(weapon.ammo, transform.GetChild(0).position, Quaternion.identity);
       ammo a = b.AddComponent<ammo>();
       a.setup(weapon.damage, weapon.projectile_speed);
       canShoot = false;
    }
}
