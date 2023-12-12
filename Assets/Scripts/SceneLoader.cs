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
	}

	public void LoadNextScene(string _sceneIndex)
	{
		SceneManager.LoadScene(_sceneIndex);
	}

	public void LoadNewGame()
	{
		LoadNextScene(sceneList[1]);
	}

	public string ActiveSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}
}
