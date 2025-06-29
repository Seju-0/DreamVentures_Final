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
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < ChoiceResults.decisions.Count; i++)
        {
            var d = ChoiceResults.decisions[i];
            sb.AppendLine($"Client: {d.clientName}");
            sb.AppendLine($"Decision: {(d.approved ? "Loan Approved" : "Loan Rejected")}");
            sb.AppendLine($"Evaluation: {d.evaluationText}");
            sb.AppendLine($"Reputation Metrics: {d.reputationMetrics}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
