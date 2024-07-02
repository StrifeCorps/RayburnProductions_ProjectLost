using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedTree : MonoBehaviour
{
    private Animator animator;
	private string currentAnimation;
	private PlayerController playerController;
    private bool isLooking;

	//Animations
	private const string TREE_IDLE = "tree_idle";
	private const string TREE_OPEN_EYE = "tree_open_eye";
	private const string TREE_EYE_LEFT = "tree_eye_left";
	private const string TREE_EYE_RIGHT = "tree_eye_right";

	// Start is called before the first frame update
	void Start()
    {
		animator = GetComponent<Animator>();
		playerController = FindAnyObjectByType<PlayerController>();

		AnimationChange(TREE_IDLE);
		StartCoroutine(ActiveEye());
    }

    // Update is called once per frame
    void Update()
    {
		if (isLooking && playerController.transform.position.x > transform.position.x)
		{ AnimationChange(TREE_EYE_RIGHT); }
		else if (isLooking && playerController.transform.position.x < transform.position.x)
		{ AnimationChange(TREE_EYE_LEFT); }
	}
	private void AnimationChange(string _nextAnimation)
	{
		if (currentAnimation == _nextAnimation) { return; }
		animator.Play(_nextAnimation);
	}

	private IEnumerator ActiveEye()
	{
		int activeTimer = Random.Range(3, 10);
		int cooldownTimer = Random.Range(10, 30);
		AnimationChange(TREE_OPEN_EYE);
		
		yield return new WaitForSeconds(activeTimer);

		isLooking = false;
		AnimationChange(TREE_IDLE);

		yield return new WaitForSeconds(cooldownTimer);

		StartCoroutine(ActiveEye());
	}

	private void ActivateLooking()
	{
		isLooking = true;
	}
}
