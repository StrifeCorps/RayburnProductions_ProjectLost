using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
	public AudioManager AudioManager { get; private set; }
	public SceneManager SceneManager { get; private set; }
	public UIManager UIManager { get; private set; }

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
