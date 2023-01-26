using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//static properties
	public static string Tag = "Player";

	//public/inspector properties
	[SerializeField] private float Speed;
	[SerializeField] private float AttackSpeed;

	//private properties
	private float timeToNextAttack;

	//unity methods
	void Update()
	{
		if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
		{
			if (timeToNextAttack <= 0)
			{
				Attack();
			}
		}


		transform.position += new Vector3(0, Input.GetAxisRaw("Vertical") * Speed, 0);

	}

	//private methods

	void Attack()
	{
		timeToNextAttack = AttackSpeed;
	}
}
