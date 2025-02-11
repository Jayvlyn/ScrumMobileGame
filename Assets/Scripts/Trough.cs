using System.Collections.Generic;
using UnityEngine;

public class Trough : MonoBehaviour
{
	private List<BallBase> ballCollection;

	private LayerMask ballLayer;

	private void Start()
	{
		ballCollection = new List<BallBase>();
		ballLayer = LayerMask.NameToLayer("Ball");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == ballLayer.value)
		{
			if (collision.gameObject.TryGetComponent(out BallBase ball))
			{
				ballCollection.Add(ball);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == ballLayer.value)
		{
			ballCollection.Remove(collision.gameObject.GetComponent<BallBase>());
		}
	}

	public void OnPlungerReleased(float force)
	{
		foreach(BallBase ball in ballCollection)
		{
			ball.rb.AddForce(transform.up * force);
		}
	}
}
