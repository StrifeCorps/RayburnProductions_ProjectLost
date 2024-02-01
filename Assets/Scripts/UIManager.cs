using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	#region Variables

	public GameManager GameManager;
    [SerializeField] GameObject mainMenu;
	[SerializeField] GameObject pauseMenu;
    private Selectable uiElementToFocus;

	#endregion

	void Start()
    {
        GameManager = GameManager.Instance;
    }

	#region UI Buttons
    public void LoadMainMenu()
    {
        ClearUI();
		GameManager.SceneLoader.LoadNextScene("lvl_MainMenu");
        StartCoroutine(MainMenuUIActivate());
	}

	public void StartNewGame()
	{
		ClearUI();
        GameManager.SceneLoader.LoadNextScene(GameManager.SceneLoader.sceneList[1]);
	}

	public void ExitFromPauseMenu()
	{
		GameManager.Instance.OnPause();
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

	#endregion

	#region UI Backend

	public void PauseUI(bool _active)
    {
        if(mainMenu.activeSelf) { return; }

        if (_active) 
        {
            SetActiveUI(pauseMenu);
            StartCoroutine(SetUIFocus(pauseMenu));
		}
        else { ClearUI(); }
	}

    public void MainMenuUI()
    {
        StartCoroutine(MainMenuUIActivate());
    }

    public void ClearUI()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    private void SetActiveUI(GameObject _menuUI)
    {
        ClearUI();
        _menuUI.SetActive(true);
    }

    IEnumerator MainMenuUIActivate()
    {
        yield return new WaitForEndOfFrame();
        SetActiveUI(mainMenu);
        StartCoroutine(SetUIFocus(mainMenu));
    }

    IEnumerator SetUIFocus(GameObject menu)
    {
        if(menu == null) {
            Debug.Log("Nothing was passed into the Ui Set focus.");
            yield break; }
        yield return new WaitForEndOfFrame();

		uiElementToFocus = menu.GetComponentInChildren<Button>();
		EventSystem.current.SetSelectedGameObject(uiElementToFocus.gameObject);
    }

    #endregion
}
