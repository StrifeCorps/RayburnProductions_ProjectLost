using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private int keys;

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
    }
}
