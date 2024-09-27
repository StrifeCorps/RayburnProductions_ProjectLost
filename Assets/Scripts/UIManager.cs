using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	#region Variables

	public GameManager GameManager;
    [SerializeField] GameObject mainMenu;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject gameoverMenu;
	[SerializeField] GameObject endGameMenu;
	[SerializeField] GameObject collectibleUI;
	[SerializeField] Slider masterAudio;
	private Selectable uiElementToFocus;
	private AudioSource audioSource;

    #endregion


    #region Initialize

    void Start()
    {
        GameManager = GameManager.Instance;
		audioSource = GetComponent<AudioSource>();
    }

	#endregion

	private void Update()
	{
        if (GameManager.state == GameManager.gameState.UI) 
        {
            if (EventSystem.current.currentSelectedGameObject == null && !EventSystem.current.IsPointerOverGameObject()) { CheckAndSetUIFocus(); }
            else if (EventSystem.current.IsPointerOverGameObject()) { EventSystem.current.SetSelectedGameObject(null); }
		}
	}

	#region UI Buttons
	public void LoadMainMenu()
    {
		collectibleUI.SetActive(false);
		GameManager.SceneLoader.LoadNextScene("lvl_MainMenu");
        StartCoroutine(MainMenuUIActivate());
		GameManager.AudioManager.PlayTrack(0);
	}

	public void StartNewGame()
	{
		ClearUI();
        GameManager.SceneLoader.LoadNextScene(GameManager.SceneLoader.sceneList[1]);
		StartCoroutine(SetCollectibleUIActive());
		GameManager.AudioManager.PlayTrack(1);
	}

	public void ExitFromPauseMenu()
	{
		GameManager.OnPause();
	}

	public void OnAudioVolumeChange()
	{
		GameManager.AudioManager.SetMasterVolume(masterAudio.value);
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
        if(mainMenu.activeSelf || endGameMenu.activeSelf) { return; }

        if (_active) 
        {
            SetActiveUI(pauseMenu);
            StartCoroutine(SetUIFocus(pauseMenu));
		}
        else { ClearUI(); }
	}

	public void GameOver(bool _active)
	{
		if (mainMenu.activeSelf) { return; }

		if (_active)
		{
			SetActiveUI(gameoverMenu);
			StartCoroutine(SetUIFocus(gameoverMenu));
		}
		else { ClearUI(); }
	}

	public void EndGame(bool _active)
	{
		if (mainMenu.activeSelf) { return; }

		if (_active)
		{
			SetActiveUI(endGameMenu);
			StartCoroutine(SetUIFocus(endGameMenu));
			GameManager.AudioManager.StopTrack();
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
        gameoverMenu.SetActive(false);
    }

    private void SetActiveUI(GameObject _menuUI)
    {
        ClearUI();
        _menuUI.SetActive(true);
    }

    public void CheckAndSetUIFocus()
    {
        if (mainMenu.activeSelf) { StartCoroutine(SetUIFocus(mainMenu)); }
        if (pauseMenu.activeSelf) { StartCoroutine(SetUIFocus(pauseMenu)); }
		if (gameoverMenu.activeSelf) { StartCoroutine(SetUIFocus(gameoverMenu)); }
		if (endGameMenu.activeSelf) { StartCoroutine(SetUIFocus(endGameMenu)); }
	}

	public void UpdateCollectibleCount(int count)
	{
		collectibleUI.GetComponentInChildren<TMP_Text>().text = count + "/5";
	}

	public void PlayUIUpdateAudio()
	{
		audioSource.Play();
	}

    IEnumerator MainMenuUIActivate()
    {
        yield return new WaitForEndOfFrame();
        SetActiveUI(mainMenu);
        StartCoroutine(SetUIFocus(mainMenu));
    }

	IEnumerator SetCollectibleUIActive()
	{
		yield return new WaitForEndOfFrame();
		collectibleUI.SetActive(true);
		UpdateCollectibleCount(0);
	}

    IEnumerator SetUIFocus(GameObject menu)
    {
        if(menu == null) {
            //Debug.Log("Nothing was passed into the Ui Set focus.");
            yield break; }
        yield return new WaitForEndOfFrame();

		uiElementToFocus = menu.GetComponentInChildren<Button>();
		EventSystem.current.SetSelectedGameObject(uiElementToFocus.gameObject);
    }

	#endregion
}
