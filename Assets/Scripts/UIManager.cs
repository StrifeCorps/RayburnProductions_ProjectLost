using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	#region Variables

	public GameManager GameManager;
    [SerializeField] GameObject mainMenu;
	[SerializeField] GameObject pauseMenu;

	#endregion

	void Start()
    {
        GameManager = GameManager.Instance;
    }

	private void Update()
	{
		if (GameManager.state == GameManager.gameState.Paused) { PauseUI(true); }
        else PauseUI(false);
	}

	public void PauseUI(bool _active)
    {
        if(mainMenu.activeSelf) { return; }

        ClearUI();
        if (_active) { pauseMenu.SetActive(true); }
        else { pauseMenu.SetActive(false); }
    }

    public void ClearUI()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        ClearUI();
		GameManager.SceneLoader.LoadNextScene("lvl_MainMenu");
        StartCoroutine(MainMenuUIActivate());
	}

    public void SetActiveUI(GameObject _menuUI)
    {
        ClearUI();
        _menuUI.SetActive(true);
    }

    public void StartNewGame()
    {
        ClearUI();
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

    IEnumerator MainMenuUIActivate()
    {
        yield return new WaitForEndOfFrame();
        SetActiveUI(mainMenu);
    }
}
