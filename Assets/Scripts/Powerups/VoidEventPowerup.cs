using UnityEngine;
using GameEvents;

public class VoidEventPowerup : Powerup
{
    [SerializeField] VoidEvent voidEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D (collision);
        if (collision.gameObject.GetComponent<BallBase>())
        {
            voidEvent?.Raise();
            Destroy(gameObject);
        }
    }
}
