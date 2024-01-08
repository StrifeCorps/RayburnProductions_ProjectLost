using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawOrderManager : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void LateUpdate()
	{
		float temp = (transform.position.y - spriteRenderer.bounds.size.y / 2)/10;
		transform.position = new(transform.position.x, transform.position.y, temp);
	}
}
