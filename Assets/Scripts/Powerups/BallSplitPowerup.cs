using UnityEngine;

public class BallSplitPowerup : Powerup
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        BallBase ball = collision.GetComponent<BallBase>();
        if (ball)
        {
            ball.OnBallSplit();
            Destroy(gameObject);
        }
    }
}
