using GameEvents;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public BaseGameEvent<BallBase> powerupEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallBase ball = collision.gameObject.GetComponent<BallBase>();
        if (collision.gameObject.GetComponent<BallBase>())
        {
            powerupEvent?.Raise(ball);
            Destroy(gameObject);
        }
    }

}
