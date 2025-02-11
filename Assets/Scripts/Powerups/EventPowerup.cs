using UnityEngine;
using GameEvents;

public class EventPowerup : MonoBehaviour
{
    [SerializeField] VoidEvent voidEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallBase>())
        {
            voidEvent?.Raise();
            Destroy(gameObject);
        }
    }
}
