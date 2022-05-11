using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player Player;

    [SerializeField] private Image MaxHealth;
    [SerializeField] private Image CurrentHealth;

	[SerializeField] private Image Slider;
	private void Update()
	{
		MaxHealth.fillAmount = Player.MaxHealthPercentage();
		CurrentHealth.fillAmount = Player.CurrentHealthPercentage();
		CurrentHealth.color = Slider.color;
	}
}
