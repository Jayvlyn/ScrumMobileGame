using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
	[SerializeField] Path[] paths;
	private Path path;
	private int pathIndex = 0;
	[SerializeField] float acceleration;
	[SerializeField] float maxSpeed;
	private Rigidbody2D rb;
	private Collider2D alrCollided;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		int r = Random.Range(0, paths.Length);
		path = paths[r];
	}

	private void Update()
	{
		Vector2 direction = new Vector2(path.points[pathIndex].position.x - transform.position.x, path.points[pathIndex].position.y - transform.position.y);
		float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, rotation);
		MoveEnemy();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PathPoint") && collision != alrCollided)
		{
			pathIndex++;
			rb.linearVelocity = Vector2.zero;
			alrCollided = collision;
		}
	}

	private void MoveEnemy()
	{
		rb.AddForce(transform.right * acceleration);

		//if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
		//{
		//	rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
		//}
	}
}
