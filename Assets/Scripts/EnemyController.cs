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
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Vector2 moveSpeed;
    private bool isChasing;
    [SerializeField] private int offsetPlayer_x, offsetPlayer_y;
    [SerializeField] private int chaseTimer;
    [SerializeField] private float speedMultiplier;
    public static event Action OnSpawn;
	public static event Action OnDespawn;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        speedMultiplier = .2f;
        isChasing = false;

        Spawn();
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

    void Spawn()
    {
        SwitchSpriteAndCollisionActiveStatus(true);
        OnSpawn?.Invoke();
        Reposition();
        StartCoroutine(ChaseCountdown());
    }
    void Despawn()
    {
        SwitchSpriteAndCollisionActiveStatus(false);
		OnDespawn?.Invoke();
        StartCoroutine(ChaseCooldown(UnityEngine.Random.Range(0,5)));
	}

    IEnumerator ChaseCountdown()
    {
        int count = 0;
        isChasing = true;

        while(count <= chaseTimer)
        {  
            count++;
            yield return new WaitForSeconds(1f);
        }

        isChasing = false;
        Despawn();
    }

    IEnumerator ChaseCooldown(int _cooldown)
    {
        int temp = 0;
        
        while( temp <= _cooldown)
        {
            yield return new WaitForSeconds(1f);
            temp++;
        }

        Spawn();
    }

    void SwitchSpriteAndCollisionActiveStatus(bool _input)
    {
        spriteRenderer.enabled = _input;
        boxCollider.enabled = _input;
    }

    void Reposition()
    {
        Vector2 playerPos = player.transform.position;

        offsetPlayer_x = Random.Range(-1, 2) * Random.Range(5, 20);
        offsetPlayer_y = Random.Range(-1, 2) * Random.Range(5, 20);
		Vector2 distanceFromPlayer = new(offsetPlayer_x, offsetPlayer_y);

		Vector2 spawnLocation = playerPos + distanceFromPlayer;
		transform.position = spawnLocation;
    }
}
