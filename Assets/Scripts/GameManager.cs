using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	#region Variables
	public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager;
	public SceneLoader SceneLoader;
	public UIManager UIManager;

	public enum gameState { Active, UI, Loading}
	public gameState state {  get; private set; }

	private Camera mainCamera;
	[SerializeField] private PlayerInput playerInput;
	#endregion

	#region Initialize
	private void Awake()
	{
		//Singleton 
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		AudioManager = FindObjectOfType<AudioManager>();
		SceneLoader = FindObjectOfType<SceneLoader>();
		UIManager = FindObjectOfType<UIManager>();
		playerInput = GetComponent<PlayerInput>();

		ResetStateToActive();
	}
	#endregion

	private void SetGameState(gameState _state)
	{
		state = _state;

		switch (state)
		{
			case gameState.Active:
				playerInput.SwitchCurrentActionMap("Player");
				break;
			case gameState.UI:
				playerInput.SwitchCurrentActionMap("UI");
				break;
			case gameState.Loading:
				break;
			default:
				Debug.Log("Game state is currently unassigned.");
				break;
		}

		//Debug.Log(playerInput.currentActionMap);
	}

	public void ResetStateToActive()
	{
		if (SceneLoader.ActiveSceneName() == "lvl_MainMenu")
		{
			SetGameState(gameState.UI);
		}
		else
		{
			SetGameState(gameState.Active);
		}
	}

	//Assigns camera script to main camera - called when a scene is loaded
	public void AssignMainCamera()
	{
		mainCamera = Camera.main;
		mainCamera.AddComponent<CameraControl>();
	}
	//Called via broadcast from input controller
	public void OnPause()
	{
		if (SceneLoader.ActiveSceneName() != "lvl_MainMenu")
		{
			if (state != gameState.Active)
			{
				SetGameState(gameState.Active);
				UIManager.PauseUI(false);
			}
			else
			{
				SetGameState(gameState.UI);
				UIManager.PauseUI(true);
			}
		}
	}
}
