using UnityEngine;
using TMPro;

public class ReputationResult : MonoBehaviour
{
    public TextMeshProUGUI reputationText;

    void Start()
    {
        if (reputationText != null)
            reputationText.text = $"Total Reputation: {ChoiceResults.currentReputation}";
    }
}
