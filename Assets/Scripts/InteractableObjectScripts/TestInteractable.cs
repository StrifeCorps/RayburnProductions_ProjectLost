using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : InteractableObject
{
	public override void DoAction()
	{
		gameObject.SetActive(false);
	}
}
