using UnityEngine;

public class BallBase : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Collider2D col;
	[SerializeField] private SpriteRenderer spriteRenderer;

	public void OnBallSplit() // Listen for ball split event
	{
		// Spawn new ball nearby (use ball pool)
	}
}
