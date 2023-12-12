using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager GameManager;
    [SerializeField] Canvas mainMenu;
	[SerializeField] Canvas pauseMenu;

	void Start()
    {
        GameManager = GameManager.Instance;
    }

    public void StartNewGame()
    {
        GameManager.SceneLoader.LoadNewGame();
    }

    public void ExitGame()
    {
        //SaveGame Before Quitting

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClearUI()
    {
        mainMenu.enabled = false;
    }
}
