using UnityEngine;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour
{
    public Slider healthSlider;

    public void OnHealthPercentRecieved(float health01)
    {
        healthSlider.value = health01;
    }
}
