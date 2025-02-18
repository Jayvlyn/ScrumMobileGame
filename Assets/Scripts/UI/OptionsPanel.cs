using UnityEngine;
using System.Collections;

public class OptionsPanel : MonoBehaviour
{
    public GameObject uiPanel;
    public Animator uiAnimator;
    private bool isOpen = false;

    public void ToggleUI()
    {
        if (isOpen)
        {
            uiAnimator.Play("OptionsClose");
            StartCoroutine(DisablePanelAfterAnimation());
        }
        else
        {
            uiPanel.SetActive(true);
            uiAnimator.Play("OptionsOpen");
        }

        isOpen = !isOpen;
    }

    private IEnumerator DisablePanelAfterAnimation()
    {
        yield return new WaitForSeconds(uiAnimator.GetCurrentAnimatorStateInfo(0).length);
        uiPanel.SetActive(false);
    }
}
