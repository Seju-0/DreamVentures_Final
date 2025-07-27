using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ReputationResult : MonoBehaviour
{
    public TextMeshProUGUI StartingReputationText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI ReputationLostText;
    public TextMeshProUGUI warningsText;

    public float animationDelay = 0.8f;

    void Start()
    {
        int startingReputation = ChoiceResults.startingReputationAtDay;
        int warnings = ChoiceResults.warningsThisDay;
        int lostFromWarnings = warnings * 2;

        // Deduct penalty BEFORE animation
        ChoiceResults.currentReputation -= lostFromWarnings;

        if (StartingReputationText != null)
            StartingReputationText.text = $"Starting Reputation: {startingReputation}";

        if (ReputationLostText != null)
            ReputationLostText.text = $"Reputation Lost due to Warnings: {lostFromWarnings}";

        if (warningsText != null)
            warningsText.text = $"Warnings Issued: {warnings}";

        if (reputationText != null)
            StartCoroutine(AnimateReputationGains(startingReputation, lostFromWarnings));
    }

    private IEnumerator AnimateReputationGains(int startingValue, int lostFromWarnings)
    {
        int runningTotal = startingValue - lostFromWarnings;
        reputationText.text = $"Total Reputation: {runningTotal}";

        List<ChoiceResults.ClientDecision> decisions = ChoiceResults.GetDecisionsForCurrentDay();

        foreach (var decision in decisions)
        {
            int change = decision.reputationValue;
            runningTotal += change;

            string sign = change >= 0 ? "+" : "";
            reputationText.text = $"Total Reputation: {runningTotal}  ({sign}{change})";

            yield return new WaitForSeconds(animationDelay);

            reputationText.text = $"Total Reputation: {runningTotal}";
        }
    }
}
