using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager;
	public SceneLoader SceneLoader;
	public UIManager UIManager;

	public enum gameState { Active, Paused}
	public gameState state {  get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		SetGameState(gameState.Active);
	}

	private void Start()
	{
		AudioManager = FindObjectOfType<AudioManager>();
		SceneLoader = FindObjectOfType<SceneLoader>();
		UIManager = FindObjectOfType<UIManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (state != gameState.Active)
			{
				SetGameState(gameState.Active);
				UIManager.PauseUI(false);
				Time.timeScale = 1;
			}
			else
			{
				SetGameState(gameState.Paused);
				UIManager.PauseUI(true);
				Time.timeScale = 0;
			}
		}
	}

	private void SetGameState(gameState _state)
	{
		state = _state;
	}
}
