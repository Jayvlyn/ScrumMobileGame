using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class WaveBasedSpawner : MonoBehaviour
{
	[SerializeField] List<EnemyData> enemySpawns = new List<EnemyData>();
	[SerializeField] float timeBetweenSpawns;
	[SerializeField] float timeBetweenRounds;
	[SerializeField] float afterRoundTBSMultiplier;
	[SerializeField] float afterRoundTBRMultiplier;
	private float ogTBS;
	private int waveNum;

	private bool alr;

	private int numToSpawn;

	private void Start()
	{
		waveNum = 1;
		ogTBS = timeBetweenSpawns;
		numToSpawn = enemySpawns[0].num;
		alr = false;
	}

	private void Update()
	{
		if (timeBetweenSpawns >= 0) timeBetweenSpawns -= Time.deltaTime;
		if (enemySpawns.Count <= 0)
		{

		}
		else
		{

			if (numToSpawn <= 0)
			{
				if (enemySpawns.Count > 1) numToSpawn = enemySpawns[1].num;
				enemySpawns.RemoveAt(0);
			}

			if (enemySpawns.Count <= 0) { }
			else if (timeBetweenSpawns <= 0 && enemySpawns[0].waveToSpawn == waveNum)
			{
				Instantiate(enemySpawns[0].enemy, transform);
				numToSpawn--;
				timeBetweenSpawns = ogTBS;
			}
			else if (enemySpawns[0].waveToSpawn != waveNum && !alr)
			{
				alr = true;
				Invoke("IncreaseRound", timeBetweenRounds);
			}
		}
	}

	private void IncreaseRound()
	{
		waveNum++;
		ogTBS = ogTBS * afterRoundTBSMultiplier;
		timeBetweenRounds = timeBetweenRounds * afterRoundTBRMultiplier;
		alr = false;
	}
}
