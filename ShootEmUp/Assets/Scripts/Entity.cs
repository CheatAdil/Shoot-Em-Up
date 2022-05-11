using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    public Vector3 velocity;
    [SerializeField] protected float max_hp;
    [SerializeField] protected float HP;
    [SerializeField] protected GameObject gibs;
    protected void GetHurt(float damage)
	{
        HP -= damage;
        if (HP <= 0) Die();
        if (HP > max_hp) HP = max_hp;
	}
    protected void Die()
	{
        Instantiate(gibs, transform.position, transform.rotation);
        Destroy(this.gameObject);
	}
}
