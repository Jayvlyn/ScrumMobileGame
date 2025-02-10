using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class Bumper : MonoBehaviour
{
    [SerializeField]
    float points = 0;
    [SerializeField, Range(0, 10)]
    float bounceForce = 10;
    [SerializeField]
    AudioClip[] bumpSounds;

    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If any object with the tag collides, it will reflect in correct direction. Use bounceForce to change the force added from reflecting.
        if (collision.gameObject.GetComponent<BallBase>())
        {
            Vector2 velocity = collision.relativeVelocity;
            Vector2 normal = -collision.GetContact(0).normal;
            Vector2 reflect = Vector2.Reflect(velocity, normal);

            collision.rigidbody.AddForce(reflect * bounceForce, ForceMode2D.Impulse);

            if (bumpSounds.Length > 0) audioSource.PlayOneShot(bumpSounds[0]);
            //TODO: Play animation, add sounds.
        }
    }
}
