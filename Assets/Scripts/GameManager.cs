using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public delegate void GameDelegate ();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;
	
	public static GameManager Instance;
	
	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countDownPage;
	public Text scoreText;
	
	int score = 0;
	bool gameOver = true;
	
	public bool GameOver { get { return gameOver; } }
	public int Score { get { return score; } }
	
    void Awake ()
    {
        Instance = this;
    }
	
	void OnEnable ()
	{
		CountDownText.OnCountDownFinished += OnCountDownFinished;
		TapController.OnPlayerDied += OnPlayerDied;
		TapController.OnPlayerScored += OnPlayerScored;
	}
	
	void OnDisable ()
	{
		CountDownText.OnCountDownFinished -= OnCountDownFinished;
		TapController.OnPlayerDied -= OnPlayerDied;
		TapController.OnPlayerScored -= OnPlayerScored;
	}
	
	void OnCountDownFinished ()
	{
		SetPageState (0);
		OnGameStarted ();
		score = 0;
		gameOver = false;
	}
	
	void OnPlayerDied ()
	{
		gameOver = true;
		int savedScore = PlayerPrefs.GetInt ("HighScore");
		if (score > savedScore)
		{
			PlayerPrefs.SetInt ("HighScore", score);
		}
		SetPageState (2);
	}
	
	void OnPlayerScored ()
	{
		score++;
		scoreText.text = score.ToString ();
	}
	
	void SetPageState (int state)
	{
		switch (state)
		{
			case 0:
				startPage.SetActive (false);
				gameOverPage.SetActive (false);
				countDownPage.SetActive (false);
				break;
				
			case 1:
				startPage.SetActive (true);
				gameOverPage.SetActive (false);
				countDownPage.SetActive (false);
				break;
				
			case 2:
				startPage.SetActive (false);
				gameOverPage.SetActive (true);
				countDownPage.SetActive (false);
				break;
			
			case 3:
				startPage.SetActive (false);
				gameOverPage.SetActive (false);
				countDownPage.SetActive (true);
				break;
		}
	}

    public void ConfirmGameOver ()
	{
		OnGameOverConfirmed ();
		scoreText.text = "0";
		SetPageState (1);
	}
	
	public void StartGame ()
	{
		SetPageState (3);
	}
}
