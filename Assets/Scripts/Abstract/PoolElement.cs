using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolElement : MonoBehaviour
{
	//public methods
	public abstract void Init();

	public void TakeFromStorage()
	{
		gameObject.SetActive(true);
	}


	public void GoToStorage(Vector3 position)
	{
		transform.position = position;
		gameObject.SetActive(false);
	}

	public abstract void Return();
}
