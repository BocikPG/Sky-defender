using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	//static
	public static string BarrierTag = "Barrier";
	//events
	public static UnityEvent EndGame = new();

	//public/inspector
	public Transform[] SpawnPositions;
	[SerializeField] private float TimeBetweenWaves;
	[SerializeField] private float TimePerRound;

	public int Lives;
	public ushort Score = 0;
	private float roundTime;


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
		roundTime += Time.deltaTime;
		if (roundTime >= TimePerRound)
		{
			EndGame.Invoke();
		}
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
		if (--Lives <= 0)
		{
			EndGame.Invoke();
		}
	}

	private void OnEnemyKill()
	{
		Score += 100;
	}
}
