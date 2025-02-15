using UnityEngine;

public class Drain : MonoBehaviour
{
	private LayerMask enemyLayer;
	private LayerMask ballLayer;

	private void Start()
	{
		enemyLayer = LayerMask.NameToLayer("Enemy");
		ballLayer = LayerMask.NameToLayer("Ball");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (enemyLayer.value == collision.gameObject.layer) // is gameobject layer in enemy layer?
		{ // Collision is enemy
			Destroy(collision.gameObject);
		}
		else if (ballLayer.value == collision.gameObject.layer) // is gameobject layer in ball layer?
		{ // Collision is ball
			GameManager.instance.OnBallDrained(collision.gameObject);
		}
	}
}
