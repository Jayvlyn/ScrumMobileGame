using UnityEngine;

public class BallBase : MonoBehaviour
{
	public Rigidbody2D rb;
	[SerializeField] private Collider2D col;
	[SerializeField] private SpriteRenderer spriteRenderer;

	[Header("Enemy Stuff")]
	[SerializeField] float damage;
	LayerMask enemyLayer;

	public void OnBallSplit() // Listen for ball split event
	{
		// Spawn new ball nearby (use ball pool)
	}

	private void Start()
	{
		enemyLayer = LayerMask.NameToLayer("Enemy");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == enemyLayer)
		{
			collision.GetComponent<EnemyBase>().ApplyDamage(damage);
		}
	}

	public void ChangeScale(float size)
	{
		GetComponent<Transform>().localScale += new Vector3( size, size, size );
	}
}
