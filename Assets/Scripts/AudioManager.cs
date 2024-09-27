using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public GameManager GameManager;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip[] musicTracks;

	private void Start()
	{
		GameManager = GameManager.Instance;
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = musicTracks[0];
		audioSource.Play();
	}

	private void OnEnable()
	{
		EnemyController.OnSpawn += LowerTrackVolume;
		EnemyController.OnDespawn += IncreaseTrackVolume;
	}

	private void OnDisable()
	{
		EnemyController.OnSpawn -= LowerTrackVolume;
		EnemyController.OnDespawn -= IncreaseTrackVolume;
	}

	public void SetMasterVolume(float value)
	{
		AudioListener.volume = value;
	}

	public void PlayTrack(int trackNumber)
	{
		StartCoroutine(SwitchTrack(musicTracks[trackNumber]));
	}

	public void StopTrack()
	{
		audioSource.Stop();
	}

	public void LowerTrackVolume()
	{
		StartCoroutine(ILowerTrackVolume());
	}

	public void IncreaseTrackVolume()
	{
		StartCoroutine(IIncreaseTrackVolume());
	}

	IEnumerator SwitchTrack(AudioClip track)
	{
		while (audioSource.volume > 0)
		{
			audioSource.volume -= .05f;
			yield return new WaitForSeconds(.01f);
		}

		audioSource.Stop();
		yield return new WaitForEndOfFrame();
		audioSource.clip = track;
		audioSource.Play();

		while (audioSource.volume < 1)
		{
			audioSource.volume += .05f;
			yield return new WaitForSeconds(.01f);
		}
	}

	IEnumerator ILowerTrackVolume()
	{
		while (audioSource.volume > .5f) {
			audioSource.volume -= .05f;
			yield return new WaitForSeconds(.2f);
		}
	}

	IEnumerator IIncreaseTrackVolume()
	{
		while (audioSource.volume < 1f)
		{
			audioSource.volume += .05f;
			yield return new WaitForSeconds(.2f);
		}
	}
}
