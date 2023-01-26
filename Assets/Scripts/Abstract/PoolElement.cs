using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolElement : MonoBehaviour
{
	//public methods
	public abstract void Init();

	public void TakeFromStorage()
	{
		enabled = true;
	}


	public void GoToStorage()
	{
		enabled = false;
	}
}
