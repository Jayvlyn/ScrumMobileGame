using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject uiPanel;
    public Animator uiAnimator;
    private bool isOpen = false;
    [SerializeField] public string openAnim;
    [SerializeField] public string closeAnim;

    public void ToggleUI()
    {
        if (isOpen)
        {
            uiAnimator.Play(closeAnim);
            StartCoroutine(DisablePanelAfterAnimation());
            Time.timeScale = 1f;
        }
        else
        {
            uiPanel.SetActive(true);
            uiAnimator.Play(openAnim);
            //Time.timeScale = 0f;
        }

        isOpen = !isOpen;
    }

    private IEnumerator DisablePanelAfterAnimation()
    {
        yield return new WaitForSeconds(uiAnimator.GetCurrentAnimatorStateInfo(0).length);
        uiPanel.SetActive(false);
    }
}
