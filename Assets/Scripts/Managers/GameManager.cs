using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//static
	public static string BarrierTag = "Barrier";

	//public/inspector
	public Transform[] SpawnPositions;
	[SerializeField] private float TimeBetweenWaves;


	//private
	private float nextWaveIn;


	//unity methods
	void Start()
	{
		Enemy.OnEnemyKill.AddListener(OnEnemyKill);
		Enemy.OnPlayerCollision.AddListener(OnPlayerCollision);
	}


	void Update()
	{
		if (nextWaveIn < 0)
		{
			SpawnWave();
			nextWaveIn = TimeBetweenWaves;
		}
		nextWaveIn -= Time.deltaTime;
	}

	//private methods

	private void SpawnWave()
	{
		int enemiesToSpawn = Random.Range(2, 5);

		var SpawnPositionsLen = SpawnPositions.Length;
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			var enemy = (Enemy)PoolManager.EnemyPool.Get();
			enemy.Spawn(SpawnPositions[Random.Range(0, SpawnPositionsLen)].position);
		}
	}
	private void OnPlayerCollision()
	{
		
	}

	private void OnEnemyKill()
	{
		
	}
}
