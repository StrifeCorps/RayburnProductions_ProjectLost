using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInteractable : InteractableObject
{
	[SerializeField] private DoorController doorController;
	[SerializeField] private GameObject item;
	[SerializeField] private AudioSource audioSource;
	private GameObject instance;
	private Animator animator;
	private string currentAnimation;
	private bool activated;

	public static event Action isCollected;

	//ANIMATIONS
	private const string TREASURE_CHEST = "treasure_chest";
	private const string TREASURE_CHEST_OPEN = "treasure_chest_open";

	private void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		activated = false;

		doorController = FindAnyObjectByType<DoorController>();
	}

	public override void DoAction()
	{
		if (activated) { return; }
		instance = Instantiate(item, transform.position, Quaternion.identity);
		AnimationChange(TREASURE_CHEST_OPEN);
		StartCoroutine(DespawnItem());
		doorController.AddKeys();
		activated = true;
		isCollected?.Invoke();
		audioSource.Play();
	}

	private void AnimationChange(string _nextAnimation)
	{
		if (currentAnimation == _nextAnimation) { return; }
		animator.Play(_nextAnimation);
		currentAnimation = _nextAnimation;
	}

	private IEnumerator DespawnItem()
	{
		int timer = 10;
		while (timer > 0)
		{
			instance.transform.Translate(Vector3.up/10);
			timer--;
			yield return new WaitForSeconds(.1f);
		}

		yield return new WaitForSeconds(3);
		instance.SetActive(false);
	}
}
