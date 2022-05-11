using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float LifeTime;
    [SerializeField] private float amount;
    private float timer;

    private void Start()
    {
        ////
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= LifeTime) Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
            ////
        }
    }
}
