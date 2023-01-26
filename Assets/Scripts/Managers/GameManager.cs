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
	public static UnityEvent<ushort> OnScoreUpdate = new();
	public static UnityEvent<int> OnLivesUpdate = new();

	//public/inspector
	public Transform[] SpawnPositions;
	[SerializeField] private float TimeBetweenWaves;
	[SerializeField] private float TimePerRound;

	public int Lives;
	public ushort Score = 0;
	public static float RoundTime { get; private set; }


	//private
	private float nextWaveIn;


	//unity methods
	IEnumerator Start()
	{
		Enemy.OnEnemyKill.AddListener(OnEnemyKill);
		Enemy.OnPlayerCollision.AddListener(OnPlayerCollision);
        yield return new WaitForEndOfFrame();
        OnScoreUpdate.Invoke(Score);
        OnLivesUpdate.Invoke(Lives);
	}


	void Update()
	{
		RoundTime += Time.deltaTime;
		if (RoundTime >= TimePerRound)
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
		OnLivesUpdate.Invoke(Lives);
	}

	private void OnEnemyKill()
	{
		Score += 100;
		OnScoreUpdate.Invoke(Score);
	}
}
