using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider1 : MonoBehaviour
{


    [SerializeField] private float min_slider_setting;
    [SerializeField] private float[] markers_borders;
    [SerializeField] private GameObject[] markers;
    private float slider_pos;
    private float slider_zero_position = -70;
    [SerializeField] private Color[] colors;
    [SerializeField] private Color[] secColors;
    private Color c;
    private int colorInd = 0;
    private Image fill_sr;
    private Image min_sr;
    private Image max_sr;


    private void Start()
    {
        PutMarkers();
    }

    private void PutMarkers()
	{
        for (int i = 0; i < 2; i++)
        {
            markers[i].transform.localPosition = new Vector2(slider_zero_position + Mathf.Abs(slider_zero_position * 2) * markers_borders[i], 0);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(0)) slider_move();
        else if (Input.GetKey(KeyCode.E) || Input.GetMouseButton(1)) slider_move(true);
        c = Color.Lerp(colors[colorInd], secColors[colorInd], Mathf.Sin((6f + 2 * colorInd) * Time.time));
        fill_sr.color = c;
        max_sr.color = c;
        min_sr.color = c;
    }
    private void slider_move(bool toTheRight = false)
	{
        /*
        if (slider_pos > slider_act_min) slider_pos -= slider_sensivity * Time.deltaTime;
        else slider_pos = slider_act_min;
        updSlider();
        */
    }
    private void updSlider()
	{

	}
}
