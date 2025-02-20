using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject uiPanel;
    public Animator uiAnimator;
    private bool isOpen = false;
    [SerializeField] public string openAnim;
    [SerializeField] public string closeAnim;

    public bool isGamePaused = false;

    public void ToggleUI()
    {
        if (isOpen)
        {
            uiAnimator.Play(closeAnim);
            StartCoroutine(DisablePanelAfterAnimation());
            //UnpauseGame();
        }
        else
        {
            uiPanel.SetActive(true);
            uiAnimator.Play(openAnim);
            //PauseGame();
        }

        isOpen = !isOpen;
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    private IEnumerator DisablePanelAfterAnimation()
    {
        yield return new WaitForSeconds(uiAnimator.GetCurrentAnimatorStateInfo(0).length);
        uiPanel.SetActive(false);
    }
}
