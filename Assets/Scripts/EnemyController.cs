using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private AudioSource audioSource;
    private Animator animator;
    private string currentAnimation;
    private bool isChasing;
    [SerializeField] private int cooldownTimer;
    [SerializeField] private int offsetPlayer_x, offsetPlayer_y;
    [SerializeField] private int chaseTimer;
    [SerializeField] private float speedMultiplier;
    public static event Action OnSpawn;
	public static event Action OnDespawn;

	//Animations
	private const string STALKER_SPAWN = "stalker_spawn";
	private const string STALKER_DESPAWN = "stalker_despawn";
	private const string STALKER_LOOK = "stalker_look";
	private const string STALKER_WALK = "stalker_walk";
	private const string STALKER_EAT = "stalker_eat";

	// Start is called before the first frame update
	void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        speedMultiplier = .1f;
        isChasing = false;

        Despawn();
    }

	private void OnEnable()
	{
        TestInteractable.isCollected += IncreaseDifficulty;
	}

	private void OnDisable()
	{
		TestInteractable.isCollected -= IncreaseDifficulty;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }
		FollowPlayer();
    }


    void FollowPlayer()
    {
        if (player != null &&isChasing) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedMultiplier);
        }
    }

    void DespawnAnimationTransition()
    {
        AnimationChange(STALKER_DESPAWN);
    }

    void Spawn()
    {
        SwitchSpriteAndCollisionActiveStatus(true);
        OnSpawn?.Invoke();
        Reposition();
        AnimationChange(STALKER_SPAWN);
        audioSource.Play();
    }
    void Despawn()
    {
        audioSource.Stop();
        AnimationChange(STALKER_WALK);
        SwitchSpriteAndCollisionActiveStatus(false);
		OnDespawn?.Invoke();
        StartCoroutine(ChaseCooldown(cooldownTimer));
	}

    IEnumerator ChaseCountdown()
    {
        int count = 0;
        isChasing = true;
        AnimationChange(STALKER_WALK);

        while(count <= chaseTimer)
        {
			while (GameManager.Instance.state != GameManager.gameState.Active) { yield return new WaitForEndOfFrame(); }

			count++;
            yield return new WaitForSeconds(1f);
        }

        isChasing = false;
        AnimationChange(STALKER_LOOK);
    }

    IEnumerator ChaseCooldown(int _cooldown)
    {
        int temp = 0;
        
        while( temp <= _cooldown)
        {
			while (GameManager.Instance.state != GameManager.gameState.Active) { yield return new WaitForEndOfFrame(); }

			yield return new WaitForSeconds(1f);
            temp++;
        }

        Spawn();
    }

    void SwitchSpriteAndCollisionActiveStatus(bool _input)
    {
        spriteRenderer.enabled = _input;
        circleCollider.enabled = _input;
    }

    void Reposition()
    {
        Vector2 playerPos = player.transform.position;

        do
        {
            offsetPlayer_x = Random.Range(-2, 2) * Random.Range(10, 20);
            offsetPlayer_y = Random.Range(-2, 2) * Random.Range(10, 20);
		}
        while (offsetPlayer_x == 0 || offsetPlayer_y == 0);
		Vector2 distanceFromPlayer = new(offsetPlayer_x, offsetPlayer_y);

		Vector2 spawnLocation = playerPos + distanceFromPlayer;
		transform.position = spawnLocation;
    }

    private void IncreaseDifficulty()
    {
        speedMultiplier *= 1.15f;
        cooldownTimer -= 3;
    }

	private void AnimationChange(string _nextAnimation)
	{
		if (currentAnimation == _nextAnimation) { return; }
		animator.Play(_nextAnimation);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.GetComponent<PlayerController>() != null) 
        {
            StopCoroutine("ChaseCountdown");
            isChasing = false;
            AnimationChange(STALKER_EAT);
        }
	}

    public void Death()
    {
		AnimationChange(STALKER_DESPAWN);
        gameObject.SetActive(false);
	}
}
