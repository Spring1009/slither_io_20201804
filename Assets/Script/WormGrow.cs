using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormGrow : MonoBehaviour
{
	public WormTail wormTail;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Food"))
		{
			Destroy(other.gameObject, 0.02f);
			wormTail.AddTail();
		}
	}
}
