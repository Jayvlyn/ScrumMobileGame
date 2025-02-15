using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public ObjectPool ballPool;
	public Drain drain;
	[SerializeField] private Transform ballStart;

	private int score = 0;
	public int Score
	{
		get { return score; }
		set
		{
			score = value;
			Assets.i.OnPlayerScoreUpdated.Raise(score);
		}
	}

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

			if(playerHealth <= 0)
			{
				Assets.i.OnPlayerDeath.Raise();
				OnPlayerDeath();
			}
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
		Score += points;
	}

	public void ResetScore()
	{
		Score = 0;
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
		PlayerHealth -= damage;
		NumberPopup.Create(new Vector3(0, -6, 0), "-" + damage, default, false, false, NumberPopup.PopupType.TAKE_DAMAGE);
	}

	private void OnPlayerDeath()
	{
		float restartTime = 3f;
		StartCoroutine(PlayerDeathTimer(restartTime));
	}

	private IEnumerator PlayerDeathTimer(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		Assets.i.DoSceneChange.Raise("MainMenu");
	}

	public void OnBallDrained(GameObject ball)
	{
		ballPool.DeactivateObject(ball);
		if (ballPool.NoneActive())
		{
			//OnLevelStart();
			SetupBall();
		}
	}
}
