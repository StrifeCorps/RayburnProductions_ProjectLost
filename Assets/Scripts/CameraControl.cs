using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private PlayerController player;
	[SerializeField] private SpriteRenderer cameraVignette;
	private IEnumerator pulseVignette;

	private void Start()
	{
		player = FindAnyObjectByType<PlayerController>();
		cameraVignette = GetComponentInChildren<SpriteRenderer>();
		cameraVignette.enabled = false;
		pulseVignette = PulseVignette();
	}

	private void OnEnable()
	{
		EnemyController.OnSpawn += AddVignette;
		EnemyController.OnDespawn += RemoveVignette;
	}

	private void OnDisable()
	{
		EnemyController.OnSpawn -= AddVignette;
		EnemyController.OnDespawn -= RemoveVignette;
	}

	public void AddVignette()
	{
		Debug.Log("You did it! Enemy Spawned");
		cameraVignette.enabled = true;
		StartCoroutine(pulseVignette);
	}

	public void RemoveVignette()
	{
		Debug.Log("Enemy despawned");
		cameraVignette.enabled = false;
		StopCoroutine(pulseVignette);
	}

	IEnumerator PulseVignette()
	{
		bool alphaDirection = false;
		Color tempColor;

		ChecklAlphaDirection(ref alphaDirection);

		while(alphaDirection)
		{
			tempColor = cameraVignette.color;
			tempColor.a -= .01f;
			cameraVignette.color = tempColor;
			ChecklAlphaDirection(ref alphaDirection);
			yield return new WaitForSeconds(.1f);
		}

		while (!alphaDirection)
		{
			tempColor = cameraVignette.color;
			tempColor.a += .01f;
			cameraVignette.color = tempColor;
			ChecklAlphaDirection(ref alphaDirection);
			yield return new WaitForSeconds(.1f);
		}

		StartCoroutine(pulseVignette);
	}

	private bool ChecklAlphaDirection(ref bool _alphaDirection)
	{
		if (cameraVignette.color.a >= .4f) { _alphaDirection = true; }
		else if (cameraVignette.color.a <= 0) { _alphaDirection = false; }

		return _alphaDirection;
	}

	private void LateUpdate()
	{
		if (player != null) { 
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		}
	}
}
