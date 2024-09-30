using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResizeSpriteToScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
	private Resolution resolution;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		resolution = Screen.currentResolution;
		ResizeToScreen();
    }

	private void LateUpdate()
	{
		if (resolution.height != Screen.height || resolution.width != Screen.width)
		{
			ResizeToScreen();
			resolution = Screen.currentResolution;
		}
	}

	private void ResizeToScreen()
	{
		float width = spriteRenderer.sprite.bounds.size.x;
		float height = spriteRenderer.sprite.bounds.size.y;

		double worldScreenHeight = Camera.main.orthographicSize * 2.0;
		double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		float tempX = (float)worldScreenWidth / width;
		float tempY = (float)worldScreenHeight / height;

		spriteRenderer.transform.localScale = new Vector3(tempX, tempY, spriteRenderer.transform.localScale.z);
	}
}
