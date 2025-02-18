using UnityEngine;

public class BallSplitPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallBase ball = collision.GetComponent<BallBase>();
        if (ball)
        {
            ball.OnBallSplit();
            Destroy(gameObject);
        }
    }
}
