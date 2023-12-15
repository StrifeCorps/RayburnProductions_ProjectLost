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

	private void Update()
	{
		if (player != null) { 
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		}
	}
}
