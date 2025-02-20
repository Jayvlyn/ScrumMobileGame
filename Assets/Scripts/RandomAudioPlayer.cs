using NUnit.Framework;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public void PlayRandomAudio()
    {
        //audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        audioSource.outputAudioMixerGroup = AudioManager.instance.audioMixer.FindMatchingGroups("SoundEffects")[0];
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }

    public void PlayRandomAudioRandomPitch(float lowerPitch, float upperPitch)
    {
        audioSource.pitch = Random.Range(lowerPitch, upperPitch);
        PlayRandomAudio();
    }
}
