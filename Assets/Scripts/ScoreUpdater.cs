using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    public void UpdateText(int score)
    {
        tmp.text = "Score: " + score;
    }
}
