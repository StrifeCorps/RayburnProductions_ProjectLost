using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogClusters : FogCloud
{
    [SerializeField] private int cloudCount;
	[SerializeField] private FogCloud cloud;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
		GenerateAlpha();
		GenerateSize();

		cloudCount = Random.Range(5, 21);
		GenerateClouds();
	}

	void GenerateClouds()
	{
		for (int i = 0; i < cloudCount; i++)
		{
			GenerateSiblings();
		}
	}

	public void GenerateSiblings()
	{
		FogCloud newCloud;
		float xDirection;
		float yDirection;

		do
		{
			xDirection = Random.Range(-1f, 1f);
			yDirection = Random.Range(-1f, 1f);
		}
		while (xDirection == 0 || yDirection == 0);

		Vector3 temp = new Vector3(spriteRenderer.bounds.size.x * xDirection, spriteRenderer.bounds.size.y * yDirection, 0);

		newCloud = Instantiate(cloud, transform.position + temp, Quaternion.identity);
		newCloud.transform.SetParent(transform);
	}
}
