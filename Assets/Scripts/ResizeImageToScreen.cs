using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeImageToScreen : MonoBehaviour
{
    [SerializeField] private Image image;

	private void OnEnable()
	{
		ResizeToScreen();
	}

	private void ResizeToScreen()
	{
		float width = image.sprite.bounds.size.x;
		float height = image.sprite.bounds.size.y;

		double worldScreenHeight = Camera.main.orthographicSize * 2.0;
		double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		float tempX = (float)worldScreenWidth / width;
		float tempY = (float)worldScreenHeight / height;

		image.rectTransform.localScale = new Vector3(tempX, tempY, image.rectTransform.localScale.z);
	}
}
