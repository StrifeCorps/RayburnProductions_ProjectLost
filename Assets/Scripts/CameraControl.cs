using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private PlayerController player;

	private void Start()
	{
		player = FindAnyObjectByType<PlayerController>();
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
	}

	public void RemoveVignette()
	{
		Debug.Log("Enemy despawned");
	}

	private void LateUpdate()
	{
		if (player != null) { 
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		}
	}
}
