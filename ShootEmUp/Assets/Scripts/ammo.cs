using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    [SerializeField] private float damage = 0;
    [SerializeField] private float speed = 0;
    private bool started = false;

    public void setup (float _damage, float _speed) 
    {
        if (!started)
        {
            damage = _damage;
            speed = _speed;
            started = true;
        }
    }
    private void Update()
    {
        if (started) transform.position += new Vector3(speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "barrier") Destroy(this.gameObject);
    }
}
