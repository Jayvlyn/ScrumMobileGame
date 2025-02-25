using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    [SerializeField] float duration = 3f;

    void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0) Destroy(gameObject);
    }
}
