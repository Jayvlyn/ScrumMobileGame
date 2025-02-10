using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    public Collider2D spawnArea;
    public float spawnMinTime = 5f;
    public float spawnMaxTime = 10f;
    public bool on = true;

    private void Start()
    {
        StartCoroutine(SpawnPowerupTimer());
    }

    IEnumerator SpawnPowerupTimer()
    {
        while(on)
        {
            yield return new WaitForSeconds(Random.Range(spawnMinTime, spawnMaxTime));
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        if (powerups.Length == 0) return;

        Vector2 spawnPosition = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));

        GameObject powerup = Instantiate(
            powerups[Random.Range(0, powerups.Length)],
            spawnPosition,
            Quaternion.identity
        );
    }
}
