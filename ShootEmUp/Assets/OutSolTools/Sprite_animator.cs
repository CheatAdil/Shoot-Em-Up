using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_animator : MonoBehaviour
{
    private SpriteRenderer self;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float period;
    [SerializeField] private int RepeatTimes; ///-1 for infinity
    private bool infinite;
    private float timer = 0;
    private float step;
    private float next_mark;
    private int current = 0;

    private void Start()
    {
        self = GetComponent<SpriteRenderer>();
        if (self == null) GetComponent<Sprite_animator>().enabled = false;
        if (sprites.Length == 0) GetComponent<Sprite_animator>().enabled = false;
        if (period == 0) GetComponent<Sprite_animator>().enabled = false;
        if (RepeatTimes == -1) infinite = true;
        else if (RepeatTimes < 0) GetComponent<Sprite_animator>().enabled = false;
        step = period / sprites.Length;
        next_mark = step;
        self.sprite = sprites[current];
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= next_mark) 
        {
            if (current != sprites.Length - 1)
            {
                next_mark += step;
                current++;
                self.sprite = sprites[current];
            }
            else 
            {
                next_mark = step;
                current = 0;
                timer = 0f;
                self.sprite = sprites[current];
                if (!infinite) RepeatTimes -= 1;
                if (RepeatTimes == 0)
                {
                    self.sprite = sprites[sprites.Length - 1];
                    GetComponent<Sprite_animator>().enabled = false;
                }
            }
        }
    }
}
