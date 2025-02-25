using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/WaveEnemy")]
public class EnemyData : ScriptableObject
{
	public int waveToSpawn;
	public GameObject enemy;
	public int num;
}
