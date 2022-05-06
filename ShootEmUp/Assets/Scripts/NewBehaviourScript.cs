using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float x;
    public float y;

	private void Update()
	{
		x = transform.position.x;
		y = transform.position.y;
	}
}
