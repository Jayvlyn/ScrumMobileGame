using System.Collections;
using UnityEngine;

public class BallBase : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;

	[Header("Enemy Stuff")]
	[SerializeField] float damage;
	LayerMask enemyLayer;

    public Vector3 originalScale = new Vector3(0.3f, 0.3f, 0.3f);
    private float sizeResetTimer = 0;
    private Coroutine sizeReset;
    private bool inPowerUp = false;

    [Header("Toggle Switch")]
    [SerializeField] private ToggleSwitch toggleSwitch;

    private void Start()
	{
		enemyLayer = LayerMask.NameToLayer("Enemy");
		trailRenderer.startWidth = transform.localScale.x;

        UpdateTrailState();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == enemyLayer)
		{
			collision.GetComponent<EnemyBase>().ApplyDamage(damage);
		}
	}

    public void OnBallSplit()
    {
        GameObject ballGameObject = GameManager.instance.ballPool.ActivateObject();

        BallBase ball = ballGameObject.GetComponent<BallBase>();

        ball.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        float scale = transform.localScale.magnitude / originalScale.magnitude;
        ball.ChangeScale(scale, sizeResetTimer);
        ball.rb.linearVelocity = rb.linearVelocity;
    }

    public void ChangeScale(float newSize, float duration)
    {
        if (!inPowerUp)
        {
            inPowerUp = true;
            transform.localScale *= newSize;
        }
        else if (sizeReset != null)
        {
            StopCoroutine(sizeReset);
        }
        sizeReset = StartCoroutine(ResetScaleAfterTime(duration));
        trailRenderer.startWidth = transform.localScale.x;
	}

    private IEnumerator ResetScaleAfterTime(float duration)
    {
        sizeResetTimer = duration;
        while (sizeResetTimer > 0)
        {
            sizeResetTimer -= Time.deltaTime;
            yield return null;
        }
        ResetScale();
    }

    private void ResetScale()
    {
        inPowerUp = false;
        transform.localScale = originalScale;
		trailRenderer.startWidth = transform.localScale.x;
	}

    private void UpdateTrailState()
    {
        if (toggleSwitch != null)
        {
            trailRenderer.enabled = toggleSwitch.CurrentValue;
        }
    }

    public void SetToggleSwitch(ToggleSwitch toggle)
    {
        toggleSwitch = toggle;
        toggleSwitch.onToggleOn.AddListener(UpdateTrailState);
        toggleSwitch.onToggleOff.AddListener(UpdateTrailState);
    }

}
