using UnityEngine;

public class ModeController : MonoBehaviour
{
    public InfiniteSpawner infiniteSpawner;
    public WaveBasedSpawner waveBasedSpawner;

    public static bool infiniteSelected = true;

    private void Start()
    {
        if(infiniteSpawner && waveBasedSpawner)
        {
            if(infiniteSelected)
            {
                infiniteSpawner.enabled = true;
                waveBasedSpawner.enabled = false;
            }
            else
            {
                waveBasedSpawner.enabled = true;
                infiniteSpawner.enabled = false;
            }
        }
    }

    public void SetInfiniteSelected(bool isInfinite)
    {
        infiniteSelected = isInfinite;
    }
}
