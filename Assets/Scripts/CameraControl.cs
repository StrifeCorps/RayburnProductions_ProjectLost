using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private PlayerController player;
	[SerializeField] private SpriteRenderer cameraVignette;
	private Resolution resolution;
	private IEnumerator pulseVignette;
	private bool runVignette;

	private void Start()
	{
		player = FindAnyObjectByType<PlayerController>();
		cameraVignette = GetComponentInChildren<SpriteRenderer>();
		pulseVignette = PulseVignette();
		runVignette = false;
		resolution = Screen.currentResolution;

		if (cameraVignette != null)
		{
			ResizeVignette();
			StartCoroutine(pulseVignette);
		}
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
		runVignette = true;
	}

	public void RemoveVignette()
	{
		runVignette = false;
	}

	IEnumerator PulseVignette()
	{
		Color tempColor;

		//Always runs - increase and decrease vignette over time
		while (true)
		{
			while (runVignette)
			{
				while (cameraVignette.color.a <= .9f)
				{
					tempColor = cameraVignette.color;
					tempColor.a += .01f;
					cameraVignette.color = tempColor;
					yield return new WaitForSeconds(.05f);
				}

				while (cameraVignette.color.a >= .6f)
				{
					tempColor = cameraVignette.color;
					tempColor.a -= .01f;
					cameraVignette.color = tempColor;
					yield return new WaitForSeconds(.05f);
				}
			}
		
			while(!runVignette && cameraVignette.color.a > 0)
			{
				tempColor = cameraVignette.color;
				tempColor.a -= .01f;
				cameraVignette.color = tempColor;
				yield return new WaitForSeconds(.01f);
			}

			yield return new WaitForSeconds(.1f);
		}
	}

	private void ResizeVignette()
	{
		float width = cameraVignette.sprite.bounds.size.x;
		float height = cameraVignette.sprite.bounds.size.y;

		double worldScreenHeight = Camera.main.orthographicSize * 2.0;
		double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		float tempX = (float)worldScreenWidth / width;
		float tempY = (float)worldScreenHeight / height;

		cameraVignette.transform.localScale = new Vector3(tempX, tempY, cameraVignette.transform.localScale.z);
	}

	private void LateUpdate()
	{
		if (player != null) { 
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		}

		if (resolution.height != Screen.height || resolution.width != Screen.width)
		{
			if(cameraVignette != null)
			{
				ResizeVignette();
				resolution = Screen.currentResolution;
			}
		}
	}
}
