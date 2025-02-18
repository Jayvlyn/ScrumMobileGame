using UnityEngine;

public class MagicPost : MonoBehaviour
{
    float durability;
    [SerializeField]
    float maxDurability = 10;

    private void Start()
    {
        Deactivate();
    }

    public void DamagePost(float damage)
    {
        durability -= damage;
        if (durability < 0) Deactivate();
    }

    public void Activate()
    {
        durability = maxDurability;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    private void Deactivate()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallBase>()) 
        {
            DamagePost(2);
        }
    }

}
