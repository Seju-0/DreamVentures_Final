using UnityEngine;
using TMPro;

public class DreamResults : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI dreamReportText;

    void Start()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < ChoiceResults.decisions.Count; i++)
        {
            var d = ChoiceResults.decisions[i];

            sb.AppendLine($"Dream {i + 1}:");
            sb.AppendLine(d.dreamDialogueText);
            sb.AppendLine(d.sanityText);
            sb.AppendLine(); // Empty line between dreams
        }

        dreamReportText.text = sb.ToString();
    }
}
