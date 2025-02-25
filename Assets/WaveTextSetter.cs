using System.Collections;
using TMPro;
using UnityEngine;

public class WaveTextSetter : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        animator.SetTrigger("Show");
        StartCoroutine(TextTimer());
    }

    public void OnWaveUpdated(int newWave)
    {
        text.text = "WAVE " + newWave;
    }

    public IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
