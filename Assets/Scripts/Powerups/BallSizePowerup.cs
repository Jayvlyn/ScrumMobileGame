using GameEvents;
using UnityEngine;

public class BallSizePowerup : MonoBehaviour
{
    [SerializeField] float sizeToIncrease = 1.0f;
    [SerializeField] float time = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallBase ball = collision.GetComponent<BallBase>();
        if (ball)
        {
            ball.ChangeScale(sizeToIncrease, time);
            Destroy(gameObject);
        }
    }
}
