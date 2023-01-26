using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	//static
	public static string BarrierTag = "Barrier";
	//events
	public UnityEvent<ushort, ushort> HighScoreScreen = new();
	public UnityEvent<ushort> OnScoreUpdate = new();
	public UnityEvent<int> OnLivesUpdate = new();
	public UnityEvent OnStart = new();

	//public/inspector
	public Transform[] SpawnPositions;
	[SerializeField] private float TimeBetweenWaves;
	[SerializeField] private float TimePerRound;

	public int Lives;
	public ushort Score = 0;
	public float RoundTime { get; private set; }
	public bool GamePaused;


	//private
	private float nextWaveIn;
	private int livesMemory;
	private ushort currHighScore;


	//unity methods
	IEnumerator Start()
	{
		currHighScore = GetCurrentHighScore();
		GamePaused = true;
		livesMemory = Lives;
		Enemy.OnEnemyKill.AddListener(OnEnemyKill);
		Enemy.OnPlayerCollision.AddListener(OnPlayerCollision);
		yield return new WaitForEndOfFrame();
		OnScoreUpdate.Invoke(Score);
		OnLivesUpdate.Invoke(Lives);
		var scores = GetScores();
		HighScoreScreen.Invoke(scores[0], scores[1]);
	}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}


	void Update()
	{
		if (GamePaused)
		{
			return;
		}
		RoundTime -= Time.deltaTime;
		if (RoundTime <= 0)
		{
			EndGameFun();
		}
		if (nextWaveIn < 0)
		{
			SpawnWave();
			nextWaveIn = TimeBetweenWaves;
		}
		nextWaveIn -= Time.deltaTime;
	}

	//public methods

	public void StartGame()
	{
		OnStart.Invoke();
		Reset();
		GamePaused = false;
	}

	//private methods

	private void EndGameFun()
	{
		GamePaused = true;
		SaveScores(Score);
		HighScoreScreen.Invoke(Score, GetCurrentHighScore());
	}

	private void Reset()
	{
		RoundTime = TimePerRound;
		Score = 0;
		Lives = livesMemory;
		PoolManager.BulletPool.Clear();
		PoolManager.EnemyPool.Clear();
		OnScoreUpdate.Invoke(Score);
		OnLivesUpdate.Invoke(Lives);
	}

	private void SaveScores(ushort score)
	{
		try
		{
			using var file = File.Open(Application.persistentDataPath + "/JSON/HighScore.txt", FileMode.Create);
			var writer = new BinaryWriter(file);
			writer.Write(score);

			if (score >= currHighScore)
				writer.Write(score);
			else
				writer.Write(currHighScore);
			file.Flush();
		}
		catch (EndOfStreamException e)
		{
			Debug.Log(e);
		}

	}
	private ushort GetCurrentHighScore()
	{
		return GetScores()[1];
	}

	private ushort[] GetScores()
	{
		ushort score = 0;
		ushort highScore = 0;

		try
		{
			using var file = File.Open(Application.persistentDataPath + "/JSON/HighScore.txt", FileMode.Open);
			var reader = new BinaryReader(file);
			score = reader.ReadUInt16();
			highScore = reader.ReadUInt16();
			file.Flush();
		}
		catch (EndOfStreamException e)
		{
			Debug.Log(e);
		}


		return new[] { score, highScore };
	}

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
			EndGameFun();
		}
		OnLivesUpdate.Invoke(Lives);
	}

	private void OnEnemyKill()
	{
		Score += 100;
		OnScoreUpdate.Invoke(Score);
	}
}
