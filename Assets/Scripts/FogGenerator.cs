using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    [SerializeField] private GameObject fogClusters;
    [SerializeField] private PlayerController player;
    private Vector2 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnFogClusters());
    }

    void GenerateSpawnLocation()
    {
        float y = Random.Range(1, 15);
        int yDirection = GenerateOneNegativeOne();
        int spawnSide = GenerateOneNegativeOne();

		spawnLocation = new Vector2 (20*spawnSide
            + player.gameObject.transform.position.x, (y* yDirection) + player.gameObject.transform.position.y);
    }

    int GenerateOneNegativeOne()
    {
        int value;

		do { value = Random.Range(-1, 2); }
		while (value == 0);

        return value;
	}

    IEnumerator SpawnFogClusters()
    {
        while (true)
        {
            GenerateSpawnLocation();

            Instantiate(fogClusters, spawnLocation, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(1,5));
        }
    }
}
