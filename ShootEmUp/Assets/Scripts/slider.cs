using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slider : MonoBehaviour
{
    [SerializeField] private float slider_setting;
    [SerializeField] private float min_slider_setting;
    [SerializeField] private float slider_pos;
    [SerializeField] private float slider_sensivity;
    [SerializeField] private Vector2 slider_min_max;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject limMin, limMax;
    [SerializeField] private GameObject fill;

    [SerializeField] private float f_dist;
    private float slider_act_min;
    private void Start()
    {
        slider_pos = slider_min_max.y;
        limMin.transform.localPosition = new Vector2(slider_min_max.x,0);
        limMax.transform.localPosition = new Vector2(slider_min_max.y, 0);
        slider_act_min = (slider_min_max.x + (min_slider_setting) * (slider_min_max.y - slider_min_max.x));

        // calculating the filling
        f_dist = (limMax.transform.GetChild(0).position - limMin.transform.GetChild(0).position).x;
        fill.transform.localPosition = new Vector3(limMin.transform.GetChild(0).position.x, 0,0);
        fill.transform.localPosition += new Vector3(f_dist / 2f, 0, 0);
        float scale = f_dist * fill.GetComponent<SpriteRenderer>().size.x;
        fill.GetComponent<SpriteRenderer>().size =  new Vector2(scale * 1.33f, fill.GetComponent<SpriteRenderer>().size.y);
        // end


        updSlider();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) slider_left();
        else if (Input.GetKey(KeyCode.E)) slider_right();
    }
    private void slider_left()
    {
        if (slider_pos > slider_act_min) slider_pos -= slider_sensivity * Time.deltaTime;
        else slider_pos = slider_act_min;
        updSlider();
    }
    private void slider_right()
    {
        if (slider_pos < slider_min_max.y) slider_pos += slider_sensivity * Time.deltaTime;
        else slider_pos = slider_min_max.y;
        updSlider();
    }
    private void updSlider() 
    {
        pointer.transform.localPosition = new Vector3(slider_pos, 0f, -1f);
        slider_setting = calculateSliderSetting();
    }
    public float GetSliderSetting() 
    {
        return slider_setting;
    }
    private float calculateSliderSetting() 
    {
        return (slider_pos - slider_min_max.x) / (slider_min_max.y - slider_min_max.x);
    }
}
