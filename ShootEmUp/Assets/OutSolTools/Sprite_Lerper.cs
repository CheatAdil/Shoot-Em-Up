using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Lerper : MonoBehaviour
{
    private SpriteRenderer self;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private float seed;
    [SerializeField] private float speed;
    [SerializeField] bool alternative_function;
    [SerializeField] bool random_seed;
    private void Start()
    {
        self = GetComponent<SpriteRenderer>();
        if (random_seed) seed = Random.Range(0f, Mathf.PI * 2);
        if (self == null)
        {
            Destroy(this.gameObject);
            return;
        }
        self.color = color1;
    }
    private void Update()
    {
        self.color = Color.Lerp(color1, color2, func(!alternative_function, seed, speed));
    }
    private float func(bool mode, float shift, float speed) 
    {
        if (mode) return Mathf.Sin(speed * Time.time + shift);
        else return Mathf.Cos(speed * Time.time + shift);
    }
}
