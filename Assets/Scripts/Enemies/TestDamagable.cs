using UnityEngine;

public class TestDamagable : MonoBehaviour
{
    [SerializeField] float health;
	[SerializeField] string damageMeTag;

	private void Update()
	{
		if (health <= 0) Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(damageMeTag))
		{
			ApplyDamage(collision.GetComponent<EnemyBase>().damage);
			Destroy(collision.gameObject);
		}
	}

	private void ApplyDamage(float d)
	{
		health -= d;
	}
}
