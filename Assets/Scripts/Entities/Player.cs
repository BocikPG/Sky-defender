using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//static properties
	public static string Tag = "Player";

	//public/inspector properties
	[SerializeField] private float speed;
	[SerializeField] private float attackSpeed;
	[SerializeField] private Rigidbody2D playerRigidbody2D;

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

		playerRigidbody2D.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * speed);

	}

	//private methods

	void Attack()
	{
		timeToNextAttack = attackSpeed;
	}
}
