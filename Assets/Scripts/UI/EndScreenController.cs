using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using TMPro;

public class EndScreenController : MonoBehaviour
{
	//const
	private const string SceneName = "MainScene";
	//public/inspector
	[SerializeField] private TextMeshProUGUI HighScore;
	[SerializeField] private TextMeshProUGUI Score;
	[SerializeField] private CanvasGroup InGameGUI;
	[SerializeField] private CanvasGroup EndGameGroup;
	[SerializeField] private Button button;


	//unity methods
	void Start()
	{
		GameManager.Instance.HighScoreScreen.AddListener(HighScoreScreen);
	}

	void Update()
	{
		if (!GameManager.Instance.GamePaused)
		{
			return;
		}
		if (Input.anyKeyDown)
		{
			OnClick();
		}


	}

	public void OnClick()
	{
		DisableGroup(EndGameGroup);
		EnableGroup(InGameGUI);
		GameManager.Instance.StartGame();
	}

	private void HighScoreScreen(ushort score, ushort highScore)
	{
		DisableGroup(InGameGUI);
		HighScore.text = $"HighScore of all time: {highScore}";
		Score.text = $"Score form last round: {score}";
		EnableGroup(EndGameGroup);
	}

	private void DisableGroup(CanvasGroup group)
	{
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}
	private void EnableGroup(CanvasGroup group)
	{
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}




}
