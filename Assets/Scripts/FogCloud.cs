using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCloud : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        GenerateAlpha();
        GenerateSize();
        GenerateSiblings();
    }

	private void FixedUpdate()
	{
        transform.Translate(Vector3.left/10 * Time.deltaTime);
	}

	void GenerateAlpha()
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = Random.Range(.2f,.8f);

        spriteRenderer.color = tempColor;
    }

    void GenerateSize()
    {
        float size = Random.Range(1.5f, 3f);
        transform.position.Scale(new Vector3(size, size, 1));
    }

    void GenerateSiblings()
    {
        FogCloud newCloud;
        int direction = 0;
        int spawnChance = Random.Range(0, 2);
        Debug.Log(spawnChance);

        do
        {
            direction = Random.Range(-1, 2);
        }
        while (direction == 0);

        Vector3 temp = new Vector3(spriteRenderer.bounds.extents.x * direction, spriteRenderer.bounds.extents.y * direction, 0);


		if (spawnChance > 0) 
        { 
            newCloud = Instantiate(this, this.transform.position + temp, Quaternion.identity);
            newCloud.transform.SetParent(transform);
        }
    }
}
