using UnityEngine;

public class MagicPost : MonoBehaviour
{
    float durability;
    [SerializeField]
    float maxDurability = 5;

    private int numCollisions;

    private void Start()
    {
        Deactivate();
    }

    private void Update()
    {
        if (numCollisions > 0)
        {
            DamagePost(Time.deltaTime);
        }
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
            DamagePost(1);
            numCollisions++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallBase>())
        {
            numCollisions--;
        }
    }

}
