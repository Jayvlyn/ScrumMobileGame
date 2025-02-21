using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallBase : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;

	[Header("Enemy Stuff")]
	[SerializeField] float baseDamage;
	LayerMask enemyLayer;

    public Vector3 originalScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Color originalColor = new Color(1,1,1,1);
    public Color bigColor = new Color(0,1,0,1);

	public Color originalTrailColor = new Color(0, 1, 1, 1);
	public Color bigTrailColor = new Color(0, 1, 1, 1);

	private float sizeResetTimer = 0;
    private Coroutine sizeReset;
    private bool inPowerUp = false;

	private void Start()
	{
        rb.mass = 1;
		enemyLayer = LayerMask.NameToLayer("Enemy");
		trailRenderer.startWidth = transform.localScale.x;
        spriteRenderer.color = originalColor;
        trailRenderer.startColor = originalTrailColor;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == enemyLayer)
		{
            int damage = Mathf.RoundToInt(rb.mass * rb.linearVelocity.magnitude * baseDamage);
			collision.GetComponent<EnemyBase>().ApplyDamage(damage);

			// PARTICLE EFFECT
			Vector2 collisionPoint = collision.ClosestPoint(transform.position);

			Vector2 direction = (transform.position - (Vector3)collisionPoint).normalized;

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			Transform particles = Instantiate(Assets.i.enemyDamageParticles, collisionPoint, Quaternion.Euler(0, 0, angle));
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
            //transform.localScale *= newSize;
            StartCoroutine(LerpScale(originalScale * newSize, 2f));
            rb.mass *= newSize;
        }
        else if (sizeReset != null)
        {
            StopCoroutine(sizeReset);
        }
        sizeReset = StartCoroutine(ResetScaleAfterTime(duration));
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
        //transform.localScale = originalScale;
        StartCoroutine(LerpScale(originalScale, 2f));
		trailRenderer.startWidth = transform.localScale.x;
        rb.mass = 1;
	}

    private IEnumerator LerpScale(Vector3 newScale, float lerpTime)
    {
        Vector3 startScale = transform.localScale;

        Color startColor = spriteRenderer.color;
        Color startTrailColor = trailRenderer.startColor;
        Color targetColor;
        Color targetTrailColor;
        if (newScale == originalScale)
        {
            targetColor = originalColor;
            targetTrailColor = originalTrailColor;

        }
        else
        {
            targetColor = bigColor;
            targetTrailColor = bigTrailColor;
        }


        float elapsedTime = 0;
        while(elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;

            transform.localScale = Vector3.Lerp(startScale, newScale, elapsedTime / lerpTime);
			trailRenderer.startWidth = transform.localScale.x;

            trailRenderer.startColor = Color.Lerp(startTrailColor, targetTrailColor, elapsedTime / lerpTime);
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / lerpTime);

			yield return null;
        }
        transform.localScale = newScale;
		trailRenderer.startWidth = transform.localScale.x;
        trailRenderer.startColor = targetTrailColor;

        spriteRenderer.color = targetColor;
	}

}
