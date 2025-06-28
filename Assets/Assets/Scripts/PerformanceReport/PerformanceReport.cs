using UnityEngine;
using TMPro;

public class PerformanceReport : MonoBehaviour
{
    public TextMeshProUGUI reportText;

    void Start()
    {
        if (reportText == null)
        {
            Debug.LogError("Report TextMeshProUGUI is not assigned!");
            return;
        }

        reportText.text = GenerateReport();
    }

    string GenerateReport()
    {
        if (ChoiceResults.decisions.Count == 0)
            return "No decisions recorded.";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < ChoiceResults.decisions.Count; i++)
        {
            var d = ChoiceResults.decisions[i];
            sb.AppendLine($"Client: {d.clientName}");
            sb.AppendLine($"Decision: {(d.approved ? "Approved" : "Rejected")}");
            sb.AppendLine($"Evaluation: {d.evaluationText}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
