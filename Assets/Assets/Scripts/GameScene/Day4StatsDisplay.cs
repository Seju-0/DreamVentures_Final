using UnityEngine;
using TMPro;

public class Day4StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI reputationText;
    [SerializeField] private TextMeshProUGUI sanityText;

    void Update()
    {
        if (reputationText != null)
            reputationText.text = $"Total Reputation: {ChoiceResults.currentReputation}";

        if (sanityText != null)
            sanityText.text = $"Total Sanity: {ChoiceResults.currentSanity}";
    }
}
