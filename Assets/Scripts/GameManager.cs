using UnityEngine;

public class GameManager : MonoBehaviour
{
	public ObjectPool ballPool;
	[SerializeField] private Transform ballStart;

	private int score = 0;
	[SerializeField] private int maxPlayerHealth = 100;
	private int playerHealth;

    public static GameManager instance;

	private void Start()
	{
		instance = this;
		OnLevelStart();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			SpawnBallInStart();
		}
	}

	public void SpawnBallInStart()
	{
		GameObject ball = ballPool.ActivateObject();
		ballPool.SetObjectPosition(ball, ballStart.position);
	}

	private void SetupBall()
	{
		ballPool.DeactivateAll();
		SpawnBallInStart();
	}

	public void AddScore(int points)
	{
		score += points;
	}

	public void ResetScore()
	{
		score = 0;
	}

	public void RefillPlayerHealth()
	{
		playerHealth = maxPlayerHealth;
	}

	public void OnLevelStart()
	{
		ResetScore();
		SetupBall();
		RefillPlayerHealth();
	}

	public void DamagePlayer(int damage)
	{
		if(playerHealth - damage < 0)
		{
			playerHealth = 0;
		}
		else
		{
			playerHealth -= damage;
		}

		if(playerHealth <= 0)
		{
			OnPlayerDeath();
		}
	}

	public void OnPlayerDeath()
	{
		// Lose game logic
	}

	public void OnBallDrained(GameObject ball)
	{
		ballPool.DeactivateObject(ball);
		if (ballPool.NoneActive())
		{
			OnLevelStart();
		}
	}
}
