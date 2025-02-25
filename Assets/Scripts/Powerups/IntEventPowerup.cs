using UnityEngine;
using GameEvents;

public class IntEventPowerup : MonoBehaviour
{
    [SerializeField] IntEvent intEvent;
    [SerializeField] int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallBase>())
        {
            intEvent?.Raise(value);
            Destroy(gameObject);
        }
    }
}
