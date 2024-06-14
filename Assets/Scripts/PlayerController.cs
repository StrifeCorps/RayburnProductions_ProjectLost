using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private InputActionReference move;
    private Vector3 movePosition;
    private bool isMoving;
    private Animator animator;
	private SpriteRenderer spriteRenderer;
	private string currentAnimation;

	//Animations
	private const string PLAYER_IDLE = "player_idle";
	private const string PLAYER_WALK = "player_walk";
	private const string PLAYER_SOUL = "player_soul";

	private void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		AnimationChange(PLAYER_SOUL);
	}

	private void Update()
	{
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (move.action.IsPressed() && !isMoving) 
		{ 
			isMoving = true; 
			AnimationChange(PLAYER_WALK);
		}
		else if (!move.action.IsPressed()) 
		{ 
			isMoving = false;
			AnimationChange(PLAYER_IDLE);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (isMoving) { 
            OnMove(); 
            transform.Translate(movePosition * moveSpeed * Time.deltaTime); 
        }
    }

    public void OnMove()
    {
        movePosition = move.action.ReadValue<Vector2>();

		if (movePosition.x < 0) { spriteRenderer.flipX = true; }
		else { spriteRenderer.flipX = false; }
	}

	private void AnimationChange(string _nextAnimation)
	{
		if (currentAnimation == _nextAnimation) { return; }
		animator.Play(_nextAnimation);
	}
}
