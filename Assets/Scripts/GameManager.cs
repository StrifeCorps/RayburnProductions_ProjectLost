using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager;
	public SceneManager SceneManager;
	public UIManager UIManager;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			AudioManager = GetComponent<AudioManager>();
			SceneManager = GetComponent<SceneManager>();
			UIManager = GetComponent<UIManager>();
		}
	}
}
