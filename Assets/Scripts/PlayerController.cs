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
	private Rigidbody2D rigidbody2D;
	private AudioSource audioSource;
	private string currentAnimation;

	public static event Action OnPlayerDeath;

	private const string ANIM_PLAYER_WALK = "anim_player_walk";
	private const string ANIM_PLAYER_DEATH = "anim_player_death";

	//Animations
	//private const string PLAYER_SOUL = "player_soul";

	private void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (move.action.IsPressed() && !isMoving) 
		{ 
			isMoving = true; 
		}
		else if (!move.action.IsPressed()) 
		{ 
			isMoving = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (isMoving) { 
            OnMove(); 
            rigidbody2D.MovePosition(transform.position + movePosition * moveSpeed * Time.deltaTime); 
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyController>())
		{
			PlayerDeathPlayAnimation();
			return;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "obj_clearlevel")
		{
			if (FindObjectOfType<EnemyController>())
			{
				FindObjectOfType<EnemyController>().Death();
			}
		}
	}

	private void PlayerDeathPlayAnimation()
	{
		AnimationChange(ANIM_PLAYER_DEATH);
		audioSource.Play();
	}

	private void PlayerDeath()
	{
		OnPlayerDeath?.Invoke();
		animator.speed = 0;
	}
}
