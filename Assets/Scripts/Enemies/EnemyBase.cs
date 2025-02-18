using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[Header("Health Stuff")]
	[SerializeField] float health;
	public float Health
	{
		get { return health; }
		set
		{
			health = value;
			if (health <= 0) OnDeath();
		}
	}

	[SerializeField] public int damage;

	[Header("Path Stuff")]
	private Path[] paths;
	private Path path;
	private int pathIndex = 0;

	[Header("Movement Stuff")]
	[SerializeField] float acceleration;
	[SerializeField] float impulse;

	[Header("Point Stuff")]
	[SerializeField] int pointValue = 10;

	private Rigidbody2D rb;
	private Collider2D alrCollided;


	private LayerMask drainLm;

	private void Start()
	{
		paths = FindObjectsByType<Path>(FindObjectsSortMode.None);
		rb = GetComponent<Rigidbody2D>();
		int r = Random.Range(0, paths.Length);
		path = paths[r];
		LookAt2D();
		MoveEnemy();
		rb.AddForce(transform.up * impulse, ForceMode2D.Impulse);
		drainLm = LayerMask.NameToLayer("Drain");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PathPoint") && collision != alrCollided)
		{
			pathIndex++;
			rb.linearVelocity = Vector2.zero;
			alrCollided = collision;
			LookAt2D();
			rb.AddForce(transform.up * impulse, ForceMode2D.Impulse);
		}

		if(collision.gameObject.layer == drainLm.value)
		{
			GameManager.instance.DamagePlayer(damage);
		}
	}

	private void MoveEnemy()
	{
		rb.AddForce(transform.up * acceleration);
	}

	private void LookAt2D()
	{
		Vector2 direction;
		if (pathIndex >= path.points.Length) // no points left
		{
			direction = GameManager.instance.drain.transform.position - transform.position;
		}
		else // use next point
		{
			direction = path.points[pathIndex].position - transform.position;
		}

		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, rotation - 90);
	}

	public void ApplyDamage(float d)
	{
		Health -= d;
		NumberPopup.Create(transform.position, d.ToString(), default, false, false, NumberPopup.PopupType.DAMAGE_ENEMY);
	}

	private void OnDeath()
	{
		GameManager.instance.AddScore(pointValue);
		NumberPopup.Create(transform.position, "+"+pointValue.ToString());
		Destroy(gameObject);
	}

	
}
