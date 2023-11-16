using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public GameManager GameManager;

	void Start()
	{
		GameManager = GameManager.Instance;
	}
}
