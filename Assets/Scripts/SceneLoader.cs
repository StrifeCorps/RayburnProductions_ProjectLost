using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public GameManager GameManager;
	public List<string> sceneList;

	//Make array of Scene names in string array

	void Start()
	{
		GameManager = GameManager.Instance;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void LoadNextScene(string _sceneIndex)
	{
		SceneManager.LoadScene(_sceneIndex);
	}

	public string ActiveSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		GameManagerOnSceneLoad();
	}

	public void GameManagerOnSceneLoad()
	{
		GameManager.ResetStateToActive();
		GameManager.AssignMainCamera();
	}
}
