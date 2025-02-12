using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[Header("Health Stuff")]
	[SerializeField] float health;
	[SerializeField] public float damage;

	[Header("Path Stuff")]
	private Path[] paths;
	private Path path;
	private int pathIndex = 0;

	[Header("Movement Stuff")]
	[SerializeField] float speed;

	private Rigidbody2D rb;
	private Collider2D alrCollided;

	private void Start()
	{
		paths = FindObjectsOfType<Path>();
		rb = GetComponent<Rigidbody2D>();
		int r = Random.Range(0, paths.Length);
		path = paths[r];
		LookAt2D();
		MoveEnemy();
	}

	private void Update()
	{
		if (health <= 0) Destroy(gameObject);
		//MoveEnemy();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PathPoint") && collision != alrCollided)
		{
			pathIndex++;
			rb.linearVelocity = Vector2.zero;
			alrCollided = collision;
			LookAt2D();
			MoveEnemy();
		}
	}

	private void MoveEnemy()
	{
		rb.AddForce(transform.up * speed);
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
		health -= d;
	}
}
