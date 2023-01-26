using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameStatsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Lives;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Time;


    void Start()
    {
        GameManager.OnLivesUpdate.AddListener(OnLivesUpdate);
        GameManager.OnScoreUpdate.AddListener(OnScoreUpdate);
    }

	private void OnScoreUpdate(ushort score)
	{
		Score.text = $"Score: {score}"; 
	}

	private void OnLivesUpdate(int lives)
	{
		Lives.text = $"Lives: {lives}";
	}

	void Update()
    {
        Time.text = GameManager.RoundTime.ToString();
    }




}
