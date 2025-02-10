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
		if ((enemyLayer & (1 << collision.gameObject.layer)) != 0) // is gameobject layer in enemy layer?
		{ // Collision is enemy
			int damage = 1;

			// Below is commented out because no enemy class yet

			//if(collision.gameObject.TryGetComponent(out Enemy e))
			//{
			//	damage = e.damage;
			//}

			GameManager.instance.DamagePlayer(damage);
		}
		else if ((ballLayer & (1 << collision.gameObject.layer)) != 0) // is gameobject layer in ball layer?
		{ // Collision is ball
			GameManager.instance.OnBallDrained(collision.gameObject);
		}
	}
}
