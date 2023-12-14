using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    private Vector3 movePosition;

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(movePosition * moveSpeed * Time.deltaTime); 
    }

    void OnMove(InputValue _input)
    {
        movePosition = _input.Get<Vector2>();
	}
}
