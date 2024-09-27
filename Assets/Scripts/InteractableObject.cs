using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private string shaderOutlineThicness;
	private bool IsBeingInteractedWith;
	private bool inputLimit = false;
	[SerializeField] private Material outlineMaterial;
	[SerializeField] private InputActionReference interact;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.material != outlineMaterial && outlineMaterial != null)
		{
			spriteRenderer.material = outlineMaterial;
		}

		if (GetComponent<DrawOrderManager>() == null)
		{
			gameObject.AddComponent<DrawOrderManager>();
		}

		shaderOutlineThicness = "_OutlineThicness";
	}

	private void Update()
	{
		if(GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (interact.action.IsPressed() && !inputLimit) { IsBeingInteractedWith = true; }
		else if (interact.action.IsPressed() && inputLimit) { return; } 
		else { IsBeingInteractedWith = false; inputLimit = false; }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if(collision.gameObject.GetComponent<PlayerController>() != null && IsBeingInteractedWith && !inputLimit)
		{
			DoAction();
			inputLimit = true;
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null)
		{
			spriteRenderer.material.SetFloat(shaderOutlineThicness, .5f);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null)
		{
			spriteRenderer.material.SetFloat(shaderOutlineThicness, 0);
		}
	}

	public virtual void DoAction()
	{
		//Debug.Log($@"{this.name} has been interacted with by the player!");
	}
}
