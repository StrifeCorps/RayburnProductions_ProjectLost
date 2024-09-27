using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVEndGame : MonoBehaviour
{
    [SerializeField] private Button endGame;

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.EndGame();
        }
	}
}
