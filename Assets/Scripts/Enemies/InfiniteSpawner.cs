using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] toSpawns;
	[SerializeField] float timeBetweenSpawns;
	private float ogTBS;
	[SerializeField] float timeBetweenRampUp;
    private float ogTBRU;

	private bool stopRamping;
    
    void Start()
    {
        ogTBS = timeBetweenSpawns;
        ogTBRU = timeBetweenRampUp;
		timeBetweenSpawns = 0;
    }

    void Update()
    {
		timeBetweenSpawns -= Time.deltaTime;
		timeBetweenRampUp -= Time.deltaTime;

		if (ogTBS <= 0.25f)
		{
			stopRamping = true;
		}

		if (timeBetweenSpawns <= 0)
		{
			// spawn enemies after certain time
			Instantiate(toSpawns[Random.Range(0, toSpawns.Length)], transform);
			timeBetweenSpawns = ogTBS;
		}
		if (timeBetweenRampUp <= 0 && !stopRamping)
		{
			// make time between spawning enemies faster, and time between ramping up enemies quicker
			// if need to add changes to enemy health and speed to ramp them up, they'd go here
			ogTBS = ogTBS * 0.875f;
			ogTBRU = ogTBRU * 0.95f;
			timeBetweenRampUp = ogTBRU;
		}
	}
}
