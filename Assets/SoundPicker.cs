using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SoundPicker : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds = new AudioClip[1];
    [SerializeField] AudioSource audioSource;

    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }

    public void PlaySound(int index)
    {
        if (index > 0 && index < sounds.Length)
        {
            audioSource.PlayOneShot(sounds[index]);
        }
        else
        {
            Debug.Log("Trying to play song with index out of bounds");
        }
    }
}
