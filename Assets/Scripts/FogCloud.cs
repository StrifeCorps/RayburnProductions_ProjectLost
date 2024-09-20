using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FogCloud : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        GenerateAlpha();
        GenerateSize();

        Debug.Log(Camera.main.pixelWidth);
    }

	void FixedUpdate()
	{
        transform.Translate(Vector3.left/10 * Time.deltaTime);
	}

	private void Update()
	{
		if (transform.position.x < (Camera.main.transform.position.x - Camera.main.pixelWidth/30)) { Destroy(gameObject); }
        else if (transform.position.x > (Camera.main.transform.position.x + Camera.main.pixelWidth/30)) { Destroy(gameObject); }
	}

	public void GenerateAlpha()
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = Random.Range(.2f,.8f);

        spriteRenderer.color = tempColor;
    }

    public void GenerateSize()
    {
        float size = Random.Range(1.5f, 3f);
        transform.position.Scale(new Vector3(size, size, 1));
    }
}
