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

	// Update is called once per frame
	void FixedUpdate()
    {
		if (GameManager.Instance.state != GameManager.gameState.Active) { return; }

		if (move.action.IsPressed()) { 
            OnMove(); 
            transform.Translate(movePosition * moveSpeed * Time.deltaTime); 
        }
    }

    public void OnMove()
    {
        movePosition = move.action.ReadValue<Vector2>();
	}
}
