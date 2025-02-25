using UnityEngine;

public class Powerup : MonoBehaviour
{
    public AudioClip powerupSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.audioSource.PlayOneShot(powerupSound);
    }
}
