using UnityEngine;
using System.Collections;

public class OptionsPanel : MonoBehaviour
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
        }
        else
        {
            uiPanel.SetActive(true);
            uiAnimator.Play(openAnim);
        }

        isOpen = !isOpen;
    }

    private IEnumerator DisablePanelAfterAnimation()
    {
        yield return new WaitForSeconds(uiAnimator.GetCurrentAnimatorStateInfo(0).length);
        uiPanel.SetActive(false);
    }
}
