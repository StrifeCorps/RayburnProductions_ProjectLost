using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager;
	public SceneLoader SceneLoader;
	public UIManager UIManager;

	public enum gameState { Active, Paused, Loading}
	public gameState state {  get; private set; }

	private Camera mainCamera;

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
		
	}

	private void SetGameState(gameState _state)
	{
		state = _state;
	}

	public void ResetStateToActivate()
	{
		state = gameState.Active;
		Time.timeScale = 1;
	}

	public void AssignMainCamera()
	{
		mainCamera = Camera.main;
		mainCamera.AddComponent<CameraControl>();
	}

	public void OnPause()
	{
		if (SceneLoader.ActiveSceneName() != "lvl_MainMenu")
		{
			if (state != gameState.Active)
			{
				SetGameState(gameState.Active);
				Time.timeScale = 1;
			}
			else
			{
				SetGameState(gameState.Paused);
				Time.timeScale = 0;
			}
		}
	}
}
