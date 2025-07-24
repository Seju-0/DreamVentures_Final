using UnityEngine;
using TMPro;

public class ReputationResult : MonoBehaviour
{
    public TextMeshProUGUI StartingReputationText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI ReputationLostText;

    void Start()
    {
        if (StartingReputationText != null)
            StartingReputationText.text = $"Starting Reputation: {ChoiceResults.startingReputationAtDay}";

        if (reputationText != null)
            reputationText.text = $"Total Reputation: {ChoiceResults.currentReputation}";

        if (ReputationLostText != null)
            ReputationLostText.text = $"Reputation Lost due to Warnings: {ChoiceResults.warningsThisDay}";
    }
}
