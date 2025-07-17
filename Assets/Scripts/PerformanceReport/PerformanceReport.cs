using UnityEngine;
using TMPro;

public class PerformanceReport : MonoBehaviour
{
    public TextMeshProUGUI reportText;

    void Start()
    {
        reportText.text = GenerateReport();
    }

    string GenerateReport()
    {
        var decisions = ChoiceResults.GetDecisionsForCurrentDay();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var d in decisions)
        {
            sb.AppendLine($"Client: {d.clientName}");
            sb.AppendLine($"Decision: {(d.approved ? "Loan Approved" : "Loan Rejected")}");
            sb.AppendLine($"Evaluation: {d.evaluationText}");
            sb.AppendLine($"Reputation: {d.reputationMetrics}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
