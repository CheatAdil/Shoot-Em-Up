using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slider : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private float slider_setting;
    [SerializeField] private float min_slider_setting;
    [SerializeField] private float slider_pos;
    [SerializeField] private float slider_sensivity;
    [SerializeField] private Vector2 slider_min_max;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject limMin, limMax;
    [SerializeField] private GameObject fill;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject[] markers;
    [SerializeField] private float[] markers_borders;
    [SerializeField] private Color[] colors;
    [SerializeField] private Color[] secColors;
    [SerializeField] private WeaponMode current_mode;
    private float f_dist;

    private Color c;
    private int colorInd = 0;

    private SpriteRenderer fill_sr;
    private SpriteRenderer min_sr;
    private SpriteRenderer max_sr;
    private float slider_act_min;

    private float maxSpeedPlayer;
    private void Start()
    {
        slider_pos = slider_min_max.y;
        limMin.transform.localPosition = new Vector2(slider_min_max.x,0);
        limMax.transform.localPosition = new Vector2(slider_min_max.y, 0);
        slider_act_min = (slider_min_max.x + (min_slider_setting) * (slider_min_max.y - slider_min_max.x));
        //markers
        float range = slider_min_max.y - slider_min_max.x;
        for (int i = 0; i < 2; i++) markers[i].transform.localPosition = new Vector2(slider_min_max.x + range * markers_borders[i], 0);
        //end

        // calculating the filling
        f_dist = (limMax.transform.GetChild(0).position - limMin.transform.GetChild(0).position).x;
        fill.transform.localPosition = new Vector3(limMin.transform.GetChild(0).position.x, 0,0);
        fill.transform.localPosition += new Vector3(f_dist / 2f, 0, 0);
        float scale = f_dist * fill.GetComponent<SpriteRenderer>().size.x;
        fill.GetComponent<SpriteRenderer>().size =  new Vector2(scale * 2.015f, fill.GetComponent<SpriteRenderer>().size.y);
        // end
        fill_sr = fill.GetComponent<SpriteRenderer>();
        max_sr = limMax.GetComponent<SpriteRenderer>();
        min_sr = limMin.GetComponent<SpriteRenderer>();
        maxSpeedPlayer = Player.GetSpeed();
        updSlider();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(0)) slider_left();
        else if (Input.GetKey(KeyCode.E) || Input.GetMouseButton(1)) slider_right();
        c = Color.Lerp(colors[colorInd], secColors[colorInd], Mathf.Sin((6f + 2 * colorInd) * Time.time));
        fill_sr.color = c;
        max_sr.color = c;
        min_sr.color = c;
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
        if (slider_setting < markers_borders[0])
        {
            current_mode = WeaponMode.harmadik;
            colorInd = 2;
        }
        else if (slider_setting < markers_borders[1])
        {
            current_mode = WeaponMode.masodik;
            colorInd = 1;
        }
        else
        {
            current_mode = WeaponMode.elso;
            colorInd = 0;
        }
        Player.SendMessage("RecieveSliderUpdate", new SliderPackage(slider_setting, current_mode));
        text.text =($"shields: {(slider_setting * 100f).ToString("F1")} %\n" +
            $"thrust: {(Player.GetSpeed()/maxSpeedPlayer * 100f).ToString("F1")} %");
    }
    public float GetSliderSetting() 
    {
        return slider_setting;
    }
    public WeaponMode GetSlider_mode() 
    {
        return current_mode;
    }
    private float calculateSliderSetting() 
    {
        return (slider_pos - slider_min_max.x) / (slider_min_max.y - slider_min_max.x);
    }
}
public class SliderPackage
{
    public SliderPackage(float set, WeaponMode md) 
    {
        setting = set;
        mode = md;
    }
    public float setting;
    public WeaponMode mode;
}
