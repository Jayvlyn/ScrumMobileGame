using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
	[Header("Health Stuff")]
	[SerializeField] float health;
	[SerializeField] float damage;

	[Header("Path Stuff")]
	[SerializeField] Path[] paths;
	private Path path;
	private int pathIndex = 0;

	[Header("Movement Stuff")]
	[SerializeField] float acceleration;
	[SerializeField] float impulse;

	private Rigidbody2D rb;
	private Collider2D alrCollided;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		int r = Random.Range(0, paths.Length);
		path = paths[r];
		LookAt2D();
		rb.AddForce(transform.right * impulse, ForceMode2D.Impulse);
	}

	private void Update()
	{
		MoveEnemy();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PathPoint") && collision != alrCollided)
		{
			pathIndex++;
			rb.linearVelocity = Vector2.zero;
			alrCollided = collision;
			LookAt2D();
			rb.AddForce(transform.right * impulse, ForceMode2D.Impulse);
		}
	}

	private void MoveEnemy()
	{
			rb.AddForce(transform.right * acceleration);
	}

	private void LookAt2D()
	{
		Vector2 direction = new Vector2(path.points[pathIndex].position.x - transform.position.x, path.points[pathIndex].position.y - transform.position.y);
		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, rotation);
	}
}
