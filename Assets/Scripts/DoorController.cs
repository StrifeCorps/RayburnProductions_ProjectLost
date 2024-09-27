using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int keys { get; private set; }

	private void Start()
	{
		keys = 0;
	}

	// Update is called once per frame
	void Update()
    {
        if (keys == 5)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddKeys()
    {
        keys++;
        GameManager.Instance.UIManager.UpdateCollectibleCount(keys);
    }
}
