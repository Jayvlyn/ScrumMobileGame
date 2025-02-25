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

    }

    public void OnWaveUpdated(int newWave)
    {
        if(WaveBasedSpawner.gameBeat)
        {
            text.text = "YOU\nWIN";
            StartCoroutine(WinTimer());
        }
        else
        {
            text.text = "WAVE " + newWave;
            StartCoroutine(TextTimer());
        }
    }

    public IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
    public IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1.5f);
        ManageScene.ChangeScn("MainMenu");
    }
}
