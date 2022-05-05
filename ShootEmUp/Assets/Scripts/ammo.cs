using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    [SerializeField] private float damage = 0;
    [SerializeField] private float speed = 0;
    private Vector3 direction;
    private bool started = false;

    public void setup (float _damage, float _speed, Vector3 _direction) 
    {
        if (!started)
        {
            damage = _damage;
            speed = _speed;
            direction = _direction;
            started = true;
        }
    }
    private void Update()
    {
        if (started) transform.position += speed * Time.deltaTime * direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "barrier") Destroy(this.gameObject);
    }
}
