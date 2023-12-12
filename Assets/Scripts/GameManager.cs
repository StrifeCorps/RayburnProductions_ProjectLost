using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager;
	public SceneLoader SceneLoader;
	public UIManager UIManager;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		AudioManager = FindObjectOfType<AudioManager>();
		SceneLoader = FindObjectOfType<SceneLoader>();
		UIManager = FindObjectOfType<UIManager>();
	}
}
