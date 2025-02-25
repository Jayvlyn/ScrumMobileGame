using UnityEngine;
using System.Collections;

public class OptionsPanel : MonoBehaviour
{
    public GameObject uiPanel;
    public Animator uiAnimator;
    private bool isOpen = false;
    [SerializeField] public string openAnim;
    [SerializeField] public string closeAnim;

	public static bool trailsEnabled = true;
	public static bool particlesEnabled = true;

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

    public void OnParticlesToggle(int trueIsOne)
    {
        if(trueIsOne == 1)
        {
            particlesEnabled = true;
        }
        else
        {
            particlesEnabled = false;
        } 
    }

	public void OnTrailsToggle(int trueIsOne)
	{
        if(trueIsOne == 1)
        {
            trailsEnabled = true;
        }
        else
        {
            trailsEnabled = false;
        }
	}
}
