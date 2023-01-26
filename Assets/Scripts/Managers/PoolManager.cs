using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager<T> : MonoBehaviour where T : PoolElement
{
	//static properties
	public static string Tag = "Player";

	//public/inspector properties
	public T BulletPrefab;
	public int BulletPoolDefaultCapacity;
	public ObjectPool<T> BulletPool;

	//private properties


	//unity methods
	void Start()
	{
        BulletPool = CreateObjectPool(BulletPrefab,BulletPoolDefaultCapacity);
	}

	//private methods

	ObjectPool<T> CreateObjectPool(T prefab, int defaultCapacity)
	{
		return new ObjectPool<T>(() =>
		{
			return Instantiate(prefab);
		},
				bullet =>
				{
					bullet.TakeFromStorage();
				},
				bullet =>
				{
					bullet.GoToStorage();
				},
				bullet =>
				{
					Destroy(bullet.gameObject);
				}, false, defaultCapacity);
	}
}
