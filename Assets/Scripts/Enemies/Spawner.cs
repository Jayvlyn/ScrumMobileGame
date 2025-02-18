using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject[] toSpawns;
	[SerializeField] float timeBetweenSpawns;
	private float og;

	private void Start()
	{
		og = timeBetweenSpawns;
	}

	private void Update()
	{
		timeBetweenSpawns -= Time.deltaTime;
		if (timeBetweenSpawns <= 0)
		{
			Instantiate(toSpawns[Random.Range(0, toSpawns.Length)], transform);
			timeBetweenSpawns = og;
		}
	}


}
