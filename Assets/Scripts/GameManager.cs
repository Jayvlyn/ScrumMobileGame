using UnityEngine;

public class GameManager : MonoBehaviour
{
	public ObjectPool ballPool;
	public Drain drain;
	[SerializeField] private Transform ballStart;

	private int score = 0;
	[SerializeField] private int maxPlayerHealth = 100;

	private int playerHealth = 0;
	private int PlayerHealth
	{
		get { return playerHealth; }

		set
		{
			playerHealth = value;
			if(playerHealth < 0) playerHealth = 0;
			else if (playerHealth > maxPlayerHealth) playerHealth = maxPlayerHealth;

			Assets.i.OnPlayerHealthUpdated.Raise(playerHealth/ (float)maxPlayerHealth);
		}
	}

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
		PlayerHealth = maxPlayerHealth;
	}

	public void OnLevelStart()
	{
		RefillPlayerHealth();
		ResetScore();
		SetupBall();
	}

	public void DamagePlayer(int damage)
	{
		if(PlayerHealth - damage < 0)
		{
			PlayerHealth = 0;
		}
		else
		{
			PlayerHealth -= damage;
		}

		if(PlayerHealth <= 0)
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
