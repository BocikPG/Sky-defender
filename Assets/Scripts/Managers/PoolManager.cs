using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
	//static properties
	public static string Tag = "Player";

	//public/inspector properties
	public Bullet BulletPrefab;
	public int BulletPoolDefaultCapacity;
	public static ObjectPool<PoolElement> BulletPool;

	//unity methods
	void Start()
	{
        BulletPool = CreateObjectPool(BulletPrefab,BulletPoolDefaultCapacity);
	}

	//private methods

	private ObjectPool<PoolElement> CreateObjectPool(PoolElement prefab, int defaultCapacity)
	{
		return new ObjectPool<PoolElement>(() =>
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
				}, true, defaultCapacity);
	}
}
