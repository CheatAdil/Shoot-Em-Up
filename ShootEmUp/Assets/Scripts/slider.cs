using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slider : MonoBehaviour
{
    [SerializeField] private Player Player;
    private float maxSpeedPlayer;

    [SerializeField] private float min_slider_setting; //Percent
    [SerializeField] private float slider_sensivity;

    [SerializeField] private float[] markers_borders;
    [SerializeField] private GameObject[] markers;
    [SerializeField] private GameObject handle;

    [SerializeField] private float slider_pos;
    private float slider_zero_position = -68;

    private WeaponMode current_mode;

    #region Colors
    [SerializeField] private Color[] colors;
    [SerializeField] private Color[] secColors;
    [SerializeField] private Image slider_background;
    private Color c;
    private int colorInd = 0;
    #endregion

    [SerializeField] private TextMeshProUGUI stat_text;

    private void Start()
    {
        slider_pos = 1f;
        PutMarkers();
        maxSpeedPlayer = Player.GetSpeed();
        updSlider();
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
        slider_background.color = c;
    }
    private void slider_move(bool toTheRight = false)
	{
        if (!toTheRight)
        {
            if (slider_pos > min_slider_setting) slider_pos -= slider_sensivity * Time.deltaTime;
            else slider_pos = min_slider_setting;
        }
		else
		{
            if (slider_pos < 1f) slider_pos += slider_sensivity * Time.deltaTime;
            else slider_pos = 1f;
        }
        updSlider();
    }
    private void updSlider()
	{
        handle.transform.localPosition = new Vector3(XPosition(slider_pos), 0f, 0f);
        current_mode = CurrentWeaponMode(slider_pos);
        colorInd = (int)current_mode;

        //
        Player.SendMessage("RecieveSliderUpdate", new SliderPackage(slider_pos, current_mode));
        if (stat_text != null)
        {
            stat_text.text = ($"shields: {(slider_pos * 100f).ToString("F1")} %\n" +
                $"thrust: {(Player.GetSpeed() / maxSpeedPlayer * 100f).ToString("F1")} %");
            stat_text.color = (Player.AtMaxHealth() ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 0f, 0f, 1f));
        }
        //
    }
    private float XPosition(float percentage) //Returns x position of where to put slider handle
	{
        return (slider_zero_position + ((Mathf.Abs(slider_zero_position * 2f)) * percentage));
	}
    private WeaponMode CurrentWeaponMode(float slider_pos)
	{
        if (slider_pos < markers_borders[0])
        {
            return WeaponMode.harmadik;
        }
        else if (slider_pos < markers_borders[1])
        {
            return WeaponMode.masodik;
        }
        else
        {
            return WeaponMode.elso;
        }
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
